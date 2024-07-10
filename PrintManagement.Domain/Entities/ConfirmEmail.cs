using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Domain.Entities
{
	public class ConfirmEmail : BaseEntity
	{
		public Guid UserId { get; set; }
		public string ConfirmCode { get; set; }
		public DateTime ExpiryTime { get; set; }
		public bool IsConfirmed { get; set; } = false;

		// khóa ngoại
		public virtual User User { get; set; }
	}
}
