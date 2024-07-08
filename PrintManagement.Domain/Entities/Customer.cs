using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

		// khóa ngoại
		public virtual List<Delivery> Deliveries { get; set; }
        public virtual List<Bill> Bills { get; set; }
        public virtual List<Project> Projects { get; set; }
    }
}
