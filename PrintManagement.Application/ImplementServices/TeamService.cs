
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Asn1.Ocsp;
using PrintManagement.Application.InterfaceServices;
using PrintManagement.Application.Payloads.Mappers.Converters;
using PrintManagement.Application.Payloads.RequestModels.TeamRequests;
using PrintManagement.Application.Payloads.ResponseModels;
using PrintManagement.Application.Payloads.Responses;
using PrintManagement.Domain.Entities;
using PrintManagement.Domain.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.ImplementServices
{
    public class TeamService : ITeamService
    {
        private readonly TeamConverter _teamConverter;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IBaseRepository<Team> _baseTeamRepository;
        private readonly IBaseRepository<User> _baseUserRepository;
        private readonly ITeamRepository _teamRepository;

        public TeamService(TeamConverter teamConverter, IHttpContextAccessor contextAccessor, IBaseRepository<Team> baseTeamRepository, IBaseRepository<User> baseUserRepository, ITeamRepository teamRepository)
        {
            _teamConverter = teamConverter;
            _contextAccessor = contextAccessor;
            _baseTeamRepository = baseTeamRepository;
            _baseUserRepository = baseUserRepository;
            _teamRepository = teamRepository;
        }

        public async Task<ResponseObject<DataResponseTeam>> CreateTeamAsync(TeamCreateRequest request, CancellationToken cancellationToken)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            try
            {
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseTeam>
                    {
                        Data = null,
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Token của người dùng chưa hợp lệ",
                    };
                }
                if (!currentUser.IsInRole("Admin"))
                {
                    return new ResponseObject<DataResponseTeam>
                    {
                        Data = null,
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Bạn không có quyền thực hiện chức năng này",
                    };
                }
                var team = new Team
                {
                    CreateTime = DateTime.Now,
                    ManagerId = request.ManagerId,
                    Name = request.Name,
                    Description = request.Description,
                    NumberOfMember = 0,
                };
                await _baseTeamRepository.CreateAsync(team, cancellationToken);
                return new ResponseObject<DataResponseTeam>
                {
                    Data = await _teamConverter.EntityToDTOAsync(team, cancellationToken),
                    Status = StatusCodes.Status201Created,
                    Message = "Thêm phòng ban thành công!!!",
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseTeam>
                {
                    Data = null,
                    Status = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                };
            }
        }

        public async Task<string> DeleteTeamAsync(TeamDeleteRequest request, CancellationToken cancellationToken)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            try
            {
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return "Token người dùng không hợp lệ";
                }
                if (!currentUser.IsInRole("Admin"))
                {
                    return "Bạn không có quyền thực hiện chức năng này";
                }
                foreach (var item in await _baseUserRepository.GetAllAsync(x => x.TeamId == request.Id, cancellationToken))
                {
                    item.TeamId = null;
                    await _baseUserRepository.UpdateAsync(item, cancellationToken);
                }
                var model = await _baseTeamRepository.GetByIdAsync(x => x.Id == request.Id, cancellationToken);
                if (model == null)
                {
                    return "Không tìm thấy phòng ban";
                }
                await _baseTeamRepository.DeleteAsync(request.Id, cancellationToken);
                return "Xóa thành công!!!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<IQueryable<DataResponseTeam>> GetAllAsync(CancellationToken cancellationToken)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return null;
            }
            if (!currentUser.IsInRole("Admin"))
            {
                return null;
            }

            var model = await _baseTeamRepository.GetAllAsync(null, cancellationToken);
            var resultList = new List<DataResponseTeam>();

            foreach (var item in model)
            {
                var dataResponseTeam = await _teamConverter.EntityToDTOAsync(item, cancellationToken);
                resultList.Add(dataResponseTeam);
            }

            return resultList.AsQueryable();
        }

        public async Task<string> SwapManagerAsync(Guid teamId, Guid managerId, CancellationToken cancellationToken)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            try
            {
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return "Token người dùng không hợp lệ";
                }
                if (!currentUser.IsInRole("Admin"))
                {
                    return "Bạn không có quyền thực hiện chức năng này";
                }
                var team = await _baseTeamRepository.GetByIdAsync(x => x.Id == teamId, cancellationToken);
                if (team == null)
                {
                    return "Không tìm thấy phòng ban";
                }
                var user = await _baseUserRepository.GetByIdAsync(x => x.Id == managerId, cancellationToken);
                if (user == null)
                {
                    return "Không tìm thấy người dùng này";
                }
                await _teamRepository.AddRoleForUserAsync(managerId, cancellationToken); // thêm role và xóa role
                team.UpdateTime = DateTime.UtcNow;
                team.ManagerId = managerId;
                await _baseTeamRepository.UpdateAsync(team, cancellationToken);
                return "Thay đổi trường phòng thành công!!!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<ResponseObject<DataResponseTeam>> UpdateTeamAsync(TeamUpdateRequest request, CancellationToken cancellationToken)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            try
            {
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseTeam>
                    {
                        Data = null,
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Token của người dùng chưa hợp lệ",
                    };
                }
                if (!currentUser.IsInRole("Admin"))
                {
                    return new ResponseObject<DataResponseTeam>
                    {
                        Data = null,
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Bạn không có quyền thực hiện chức năng này",
                    };
                }
                var result = await _baseTeamRepository.GetByIdAsync(x => x.Id == request.Id, cancellationToken);
                if (request == null)
                {
                    return new ResponseObject<DataResponseTeam>
                    {
                        Data = null,
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Không tìm thấy phòng ban"
                    };
                }
                result.Name = request.Name;
                result.Description = request.Description;
                result.UpdateTime = DateTime.Now;
                var model = await _baseTeamRepository.UpdateAsync(result, cancellationToken);
                return new ResponseObject<DataResponseTeam>
                {
                    Data = await _teamConverter.EntityToDTOAsync(model,cancellationToken),
                    Status = StatusCodes.Status500InternalServerError,
                    Message = "Cập nhật phòng ban thành công!!!",
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseTeam>
                {
                    Data = null,
                    Status = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                };
            }
        }
    }
}
