using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Domain.Entities
{
	public class ResourcePropertyDetail : BaseEntity
	{
        public int PropertyId { get; set; }
        public string PropertyDetailName { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

		// khóa ngoại
		public virtual List<ImportCoupon> ImportCoupons { get; set; }
        public virtual List<ResourceForPrintJob> ResourceForPrintJobs { get; set;}
        public virtual ResourceProperty ResourceProperty { get; set; }
    }
}
