using PrintManagement.Domain.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Domain.Entities
{
    public class KeyPerformanceIndicators : BaseEntity
    {
        public int EmployeeId { get; set; } // KPI cua nhan vien theo id
        public string IndicatorName { get; set; } // Ten chi so kpi
        public int Target { get; set; } // So Project duoc giao den tay khach hang
        public int ActuallyAchieved { get; set; } // So Project dat duoc thuc te
        public Period Period { get; set; } // KPI theo thang or quy or nam
        public bool AchieveKPI { get; set; } // Có hoàn thành KPI hay không

		// khóa ngoại
		public virtual User User { get; set; }
	}
}
