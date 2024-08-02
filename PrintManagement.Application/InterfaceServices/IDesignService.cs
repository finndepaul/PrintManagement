using PrintManagement.Application.Payloads.RequestModels.DesignRequests;
using PrintManagement.Application.Payloads.ResponseModels;
using PrintManagement.Application.Payloads.Responses;
using PrintManagement.Domain.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.InterfaceServices
{
    public interface IDesignService
    {
        Task<ResponseObject<DataResponseDesign>> UploadFile(UploadFileRequest request, Guid designerId, CancellationToken cancellationToken);
        Task<IQueryable<DataResponseDesign>> GetAllDesign(CancellationToken cancellationToken);
        Task<string> ApproveFile(Guid projectId, Guid approverId, ApproveFileRequest status, CancellationToken cancellationToken);
    }
}
