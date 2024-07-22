using Microsoft.AspNetCore.Http;
using PrintManagement.Application.InterfaceServices;
using PrintManagement.Application.Payloads.Mappers.Converters;
using PrintManagement.Application.Payloads.RequestModels.ProjectRequests;
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
    public class ProjectService : IProjectService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IBaseRepository<Project> _baseProjectRepository;
        private readonly ProjectConverter _projectConverter;
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IHttpContextAccessor contextAccessor, IBaseRepository<Project> baseProjectRepository, ProjectConverter projectConverter, IProjectRepository projectRepository)
        {
            _contextAccessor = contextAccessor;
            _baseProjectRepository = baseProjectRepository;
            _projectConverter = projectConverter;
            _projectRepository = projectRepository;
        }

        public async Task<ResponseObject<DataResponseProject>> CreateProjectAsync(ProjectCreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (!await _projectRepository.CheckTeamOfLeaderAsync(request.EmployeeId, cancellationToken))
                {
                    return new ResponseObject<DataResponseProject>
                    {
                        Data = null,
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Không tìm thấy Leader mà bạn chọn",
                    };
                }
                if (!await _projectRepository.CheckTeamOfLeaderAsync(request.EmployeeId, cancellationToken))
                {
                    return new ResponseObject<DataResponseProject>
                    {
                        Data = null,
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Vui lòng chọn leader thuộc phòng ban technical!!!",
                    };
                }
                var currentUser = _contextAccessor.HttpContext.User;
                var teamOfUser = currentUser.Claims.FirstOrDefault(x => x.Type == "TeamId");
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseProject>
                    {
                        Data = null,
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Người dùng chưa được xác thực",
                    };
                }
                if (currentUser.IsInRole("Employee") && teamOfUser.Value == ("6f8624c1-d28f-43f3-9465-bee9d20ecbf6"))
                {
                    var project = new Project
                    {
                        CustomerId = request.CustomerId,
                        EmployeeId = request.EmployeeId,
                        ProjectName = request.ProjectName,
                        ProjectStatus = Domain.Enumerates.ProjectStatus.Design,
                        RequestDescriptionFromCustomer = request.RequestDescriptionFromCustomer,
                        StartDate = DateTime.Now,
                    };
                    var model = await _baseProjectRepository.CreateAsync(project, cancellationToken);
                    await _projectRepository.AddRolesToUserAsync(request.EmployeeId, cancellationToken); // thêm quyền leader nếu chưa có
                    return new ResponseObject<DataResponseProject>
                    {
                        Data = await _projectConverter.EntityToDTOAsync(project, cancellationToken),
                        Status = StatusCodes.Status201Created,
                        Message = "Tạo dự án thành công"
                    };
                }
                else
                {
                    return new ResponseObject<DataResponseProject>
                    {
                        Data = null,
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Người dùng không có quyền thực hiện chức năng này!!!",
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseProject>
                {
                    Data = null,
                    Status = StatusCodes.Status500InternalServerError,
                    Message = "Error:" + ex.Message,
                };
            }
        }

        public async Task<IQueryable<DataResponseProject>> GetAllProject(CancellationToken cancellationToken)
        {
            try
            {
                var model = await _baseProjectRepository.GetAllAsync(null, cancellationToken);
                List<DataResponseProject> lstDTO = new List<DataResponseProject>();
                foreach (var item in model)
                {
                    DataResponseProject projectItem = await _projectConverter.EntityToDTOAsync(item, cancellationToken);
                    lstDTO.Add(projectItem);
                }
                return lstDTO.AsQueryable();
            }
            catch
            {
                return null;
            }
        }
    }
}
