using PrintManagement.Domain.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Domain.Entities
{
	public class PrintJobs : BaseEntity
	{
        public Guid DesignId { get; set; }
        public PrintJobStatus PrintJobStatus { get; set; }

		// khóa ngoại
		public virtual Design Design { get; set; }
        public virtual List<ResourceForPrintJob> ResourceForPrintJobs { get; set; }
    }
}
