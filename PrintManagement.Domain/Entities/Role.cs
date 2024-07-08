using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Domain.Entities
{
    public class Role : BaseEntity
    {
        //Admin, Designer, Printer Operator
        public string RoleCode { get; set; }
        public string RoleName { get; set; }

		// khóa ngoại
		public virtual List<Permissions> Permissions { get; set; }
    }
}
