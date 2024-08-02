using PrintManagement.Domain.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.Payloads.RequestModels.ProjectRequests
{
    public class ProjectCreateRequest
    {
        public string ProjectName { get; set; }
        public string RequestDescriptionFromCustomer { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public Guid CustomerId { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
