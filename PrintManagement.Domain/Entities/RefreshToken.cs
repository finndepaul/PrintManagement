using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Domain.Entities
{
	public class RefreshToken : BaseEntity
	{
		public Guid UserId { get; set; }
		public string Token { get; set; }
		public DateTime ExpiryTime { get; set; }

		// khóa ngoại
		public virtual User User { get; set; }
	}
}
