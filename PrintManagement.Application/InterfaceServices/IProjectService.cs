using PrintManagement.Application.Payloads.RequestModels.ProjectRequests;
using PrintManagement.Application.Payloads.ResponseModels;
using PrintManagement.Application.Payloads.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.InterfaceServices
{
    public interface IProjectService
    {
        Task<IQueryable<DataResponseProject>> GetAllProject(CancellationToken cancellationToken);
        Task<DataResponseProjectDetail> GetProjectById(Guid projectId, CancellationToken cancellationToken);
        Task<ResponseObject<DataResponseProject>> CreateProject(ProjectCreateRequest request, CancellationToken cancellationToken);
    }
}
