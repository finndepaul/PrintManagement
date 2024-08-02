using PrintManagement.Application.Payloads.RequestModels.DeliveryRequests;
using PrintManagement.Application.Payloads.ResponseModels;
using PrintManagement.Domain.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.InterfaceServices
{
    public interface IDeliveryService
    {
        Task<IQueryable<DataResponseDelivery>> GetDelivererForBill(Guid projectId, CancellationToken cancellationToken);
        Task<string> DeliveryForEmployee(DeliveryForEmployeeRequest request, CancellationToken cancellationToken);
        Task<IQueryable<DataResponseBill>> GetAllBill(Guid userId, CancellationToken cancellationToken); 
        Task<string> AcceptBill(Guid billId, bool IsAccept, CancellationToken cancellationToken);
        Task ConfirmBill(Guid billId, DeliveryConfirmRequest deliveryStatus, CancellationToken cancellationToken);
    }
}
