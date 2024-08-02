using PrintManagement.Domain.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.Payloads.ResponseModels
{
    public class DataResponseBill
    {
        public string BillName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string ShippingMethodName { get; set; }
        public string EmployeeName { get; set; }   
        public string BillStatus { get; set; }
    }
}
