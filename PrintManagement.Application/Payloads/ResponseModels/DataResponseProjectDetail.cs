using PrintManagement.Domain.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.Payloads.ResponseModels
{
    public class DataResponseProjectDetail
    {
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public string RequestDescriptionFromCustomer { get; set; }
        public string LeaderName { get; set; }
        public string LeaderPhoneNumber { get; set; }
        public string LeaderEmail { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerEmail { get; set; }
    }
}
