using PrintManagement.Application.Payloads.RequestModels.CustomerRequests;
using PrintManagement.Application.Payloads.ResponseModels;
using PrintManagement.Application.Payloads.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.InterfaceServices
{
    public interface ICustomerService
    {
        Task<IQueryable<DataResponseCustomer>> GetAllCustomer(CancellationToken cancellationToken);
        Task<ResponseObject<DataResponseCustomer>> CreateCustomerAsync(CustomerCreateRequest request, CancellationToken cancellationToken);
    }
}
