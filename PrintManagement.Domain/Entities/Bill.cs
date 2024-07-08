using PrintManagement.Domain.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Domain.Entities
{
	public class Bill : BaseEntity
	{
        public string BillName { get; set; }
        public BillStatus BillStatus { get; set; }
        public decimal TotalMoney { get; set; }
        public int ProjectId { get; set; }
        public int CustomerId { get; set; }
        public string TradingCode { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int EmployeeId { get; set; }

		// khóa ngoại
		public virtual Customer Customer { get; set; }
        public virtual User User { get; set; }
    }
}
