using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.Payloads.RequestModels.ResourseForPrintjobRequest
{
    public class ResourseRequest
    {
        public Guid Resourse { get; set; }
        public int Quantity { get; set; }
    }
}
