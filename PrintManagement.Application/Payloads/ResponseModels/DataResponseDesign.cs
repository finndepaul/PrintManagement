using PrintManagement.Domain.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.Payloads.ResponseModels
{
    public class DataResponseDesign
    {
        public Guid Id { get; set; }
        public string DesignerName { get; set; }
        public string FilePath { get; set; }
        public DateTime DesignTime { get; set; }
        public string DesignStatus { get; set; }
    }
}
