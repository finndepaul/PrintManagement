using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.Payloads.RequestModels.DeliveryRequests
{
    public enum DeliveryConfirmRequest
    {
        NotReceived = 2,
        Refuse = 3,
        Received = 4,
    }
}
