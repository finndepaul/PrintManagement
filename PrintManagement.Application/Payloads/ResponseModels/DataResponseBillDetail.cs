using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.Payloads.ResponseModels
{
    public class DataResponseBillDetail
    {
        public Guid Id { get; set; }
        public decimal BillPrice { get; set; }
        public string BillName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSDT { get; set; }
        public string CustomerAddress { get; set; }
        public string ShippingMethodName { get; set; }
        public string EmployeeName { get; set; }
        public string BillStatus { get; set; }
    }
}
