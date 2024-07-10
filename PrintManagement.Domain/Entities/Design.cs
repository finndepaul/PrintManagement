using PrintManagement.Domain.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Domain.Entities
{
	public class Design : BaseEntity
	{
        public Guid ProjectId { get; set; }
        public Guid DesignerId { get; set; }
        public string FilePath { get; set; }
        public DateTime DesignTime { get; set; }
        public DesignStatus DesignStatus { get; set; }
        public Guid ApproverId { get; set; } // Người phê duyệt thiết kế

		// khóa ngoại
		public virtual User User { get; set; }
        public virtual Project Project { get; set; }
		public virtual List<PrintJobs> PrintJobs { get; set; }
	}
}
