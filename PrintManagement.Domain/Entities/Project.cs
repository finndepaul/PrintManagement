using PrintManagement.Domain.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Domain.Entities
{
    public class Project : BaseEntity
    {
        public string ProjectName { get; set; }
        public string RequestDescriptionFromCustomer { get; set; }
        public DateTime StartDate { get; set; }
        public int EmployeeId { get; set; } //Nhân viên phụ trách project
        public DateTime ExpectedEndDate { get; set; }
        public int CustomerId { get; set; }
        public ProjectStatus ProjectStatus { get; set; } //đang thiết kế, đang in, đã hoàn thành

		// khóa ngoại
        public virtual User User { get; set; }
        public virtual List<Design> Designs { get; set; }
		public virtual List<Delivery> Deliveries { get; set; }
		public virtual Customer Customer { get; set; }
    }
}
