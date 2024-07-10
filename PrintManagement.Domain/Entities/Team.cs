using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Domain.Entities
{
	public class Team : BaseEntity
	{
        public string Name { get; set; }
		public string Description { get; set; }
        public int NumberOfMember { get; set; }
		public DateTime CreateTime { get; set; }
		public DateTime UpdateTime { get; set; }
        public Guid ManagerId { get; set; }

		// khóa ngoại
		public virtual List<User> Users { get; set; }
    }
}
