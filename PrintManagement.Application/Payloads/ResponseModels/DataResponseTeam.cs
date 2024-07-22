using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.Payloads.ResponseModels
{
    public class DataResponseTeam
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfMember { get; set; }
        public string ManagerName { get; set; }
    }
}
