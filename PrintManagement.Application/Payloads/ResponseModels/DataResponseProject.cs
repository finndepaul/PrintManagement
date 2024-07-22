using PrintManagement.Domain.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.Payloads.ResponseModels
{
    public class DataResponseProject
    {
        public string ProjectName { get; set; }
        public string RequestDescriptionFromCustomer { get; set; }
        public DateTime StartDate { get; set; }
        public string EmployeeName { get; set; } //Nhân viên phụ trách project
        public DateTime ExpectedEndDate { get; set; }
        public string CustomerName { get; set; }
        public string ProjectStatus { get; set; } //đang thiết kế, đang in, đã hoàn thành
    }
}
