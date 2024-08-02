using PrintManagement.Domain.Entities;
using PrintManagement.Domain.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.Payloads.RequestModels.ResourseForPrintjobRequest
{
    public class ResourseForPrintjobCreateRequest
    {
        public Guid PrintJobId { get; set; }
        public Guid Machine { get; set; }
        public List<ResourseRequest> ResourseRequests { get; set; }
    }
}
