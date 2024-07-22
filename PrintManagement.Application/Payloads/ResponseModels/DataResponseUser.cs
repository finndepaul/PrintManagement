using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.Payloads.ResponseModels
{
    public class DataResponseUser
    {
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string TeamName { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTIme { get; set; }
        public bool IsActive { get; set; }
    }
}
