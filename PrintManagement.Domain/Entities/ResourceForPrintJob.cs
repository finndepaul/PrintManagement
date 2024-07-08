﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Domain.Entities
{
	public class ResourceForPrintJob : BaseEntity
	{
        public int ResourcePropertyDetailId { get; set; }
        public int PrintJobId { get; set; }

		// khóa ngoại
		public virtual ResourcePropertyDetail ResourcePropertyDetail { get; set; }
        public virtual PrintJobs PrintJobs { get; set; }
    }
}
