﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Domain.Entities
{
    public class Permissions : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

		// khóa ngoại
		public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
