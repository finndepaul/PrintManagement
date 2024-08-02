using PrintManagement.Domain.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.Payloads.RequestModels.DeliveryRequests
{
    public class DeliveryForEmployeeRequest
    {
        public Guid ProjectId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime EstimateDeliveryTime { get; set; }
    }
}
