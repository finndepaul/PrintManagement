using Microsoft.AspNetCore.Http;
using PrintManagement.Application.Handle.HandleFile;
using PrintManagement.Application.InterfaceServices;
using PrintManagement.Application.Payloads.Mappers.Converters;
using PrintManagement.Application.Payloads.RequestModels.DesignRequests;
using PrintManagement.Application.Payloads.ResponseModels;
using PrintManagement.Application.Payloads.Responses;
using PrintManagement.Domain.Entities;
using PrintManagement.Domain.Enumerates;
using PrintManagement.Domain.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.ImplementServices
{
    public class DesignService : IDesignService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IBaseRepository<Design> _baseDesignRepository;
        private readonly IBaseRepository<PrintJobs> _basePrintJobsRepository;
        private readonly IBaseRepository<Project> _baseProjectRepository;
        private readonly DesignConverter _designConverter;
        public DesignService(IHttpContextAccessor contextAccessor, IBaseRepository<Design> baseDesignRepository, DesignConverter designConverter, IBaseRepository<PrintJobs> basePrintJobsRepository, IBaseRepository<Project> baseProjectRepository)
        {
            _contextAccessor = contextAccessor;
            _baseDesignRepository = baseDesignRepository;
            _designConverter = designConverter;
            _basePrintJobsRepository = basePrintJobsRepository;
            _baseProjectRepository = baseProjectRepository;
        }

        public async Task<string> ApproveFile(Guid projectId, Guid approverId, ApproveFileRequest status, CancellationToken cancellationToken)
        {
            try
            {
                var currentUser = _contextAccessor.HttpContext.User;
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return "Token của người dùng không hợp lệ";
                }
                if (!currentUser.IsInRole("Leader"))
                {
                    return "Người dùng không có quyền thực hiện chức năng này";
                }
                var design = await _baseDesignRepository.GetByIdAsync(x => x.ProjectId == projectId, cancellationToken);
                if (status.ToString() == DesignStatus.Approved.ToString())
                {
                    design.ApproverId = approverId;
                    design.DesignStatus = DesignStatus.Approved;
                    await _baseDesignRepository.UpdateAsync(design, cancellationToken);

                    var printJob = new PrintJobs
                    {
                        DesignId = design.Id,
                        PrintJobStatus = Domain.Enumerates.PrintJobStatus.Waiting,
                    };
                    await _basePrintJobsRepository.CreateAsync(printJob, cancellationToken);

                    var project = await _baseProjectRepository.GetByIdAsync(x => x.Id == projectId, cancellationToken);
                    project.ProjectStatus = ProjectStatus.Printing;
                    await _baseProjectRepository.UpdateAsync(project, cancellationToken);
                }
                else
                {
                    design.ApproverId = approverId;
                    design.DesignStatus = DesignStatus.Refuse;
                    await _baseDesignRepository.UpdateAsync(design, cancellationToken);
                }
                return "Phê duyệt thành công!!!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<IQueryable<DataResponseDesign>> GetAllDesign(CancellationToken cancellationToken)
        {
            List<DataResponseDesign> lst = new List<DataResponseDesign>();
            var model = await _baseDesignRepository.GetAllAsync(null, cancellationToken);
            foreach (var item in model)
            {
                DataResponseDesign dataResponseDesign = await _designConverter.EntityToDto(item, cancellationToken);
                lst.Add(dataResponseDesign);
            }
            return lst.AsQueryable();
        }

        public async Task<ResponseObject<DataResponseDesign>> UploadFile(UploadFileRequest request, Guid designerId, CancellationToken cancellationToken)
        {
            try
            {
                var currentUser = _contextAccessor.HttpContext.User;
                var teamOfUser = currentUser.Claims.FirstOrDefault(x => x.Type == "TeamId");
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseDesign>
                    {
                        Data = null,
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Token của người dùng không hợp lệ",
                    };
                }
                if (currentUser.IsInRole("Employee") && teamOfUser.Value == ("471C942C-C8DC-4909-90DE-3B1F153212AA").ToLower())
                {
                    var lstDesigns = await _baseDesignRepository.GetAllAsync(x => x.ProjectId == request.ProjectId, cancellationToken);
                    foreach (var item in lstDesigns)
                    {
                        if (item.DesignStatus == DesignStatus.Approved)
                        {
                            return new ResponseObject<DataResponseDesign>
                            {
                                Data = null,
                                Status = StatusCodes.Status400BadRequest,
                                Message = "Đã có thiết kế được phê duyệt !!!",
                            };
                        }
                    }
                    var design = new Design
                    {
                        ProjectId = request.ProjectId,
                        FilePath = await HandleUploadFile.WriteFile(request.FilePath),
                        DesignTime = DateTime.Now,
                        DesignStatus = Domain.Enumerates.DesignStatus.NotYetApproved,
                        DesignerId = designerId,
                    };
                    await _baseDesignRepository.CreateAsync(design, cancellationToken);      
                    return new ResponseObject<DataResponseDesign>
                    {
                        Data = await _designConverter.EntityToDto(design, cancellationToken),
                        Status = StatusCodes.Status201Created,
                        Message = "Upload file thành công",
                    };
                }
                else
                {
                    return new ResponseObject<DataResponseDesign>
                    {
                        Data = null,
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Người dùng không có quyền thực hiện chức năng này",
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseDesign>
                {
                    Data = null,
                    Status = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                };
            }
        }
    }
}
