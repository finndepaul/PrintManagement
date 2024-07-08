using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Domain.Entities
{
	public class ResourceProperty : BaseEntity
	{
		public string ResourcePropertyName { get; set; }
        public int ResourceId { get; set; }
        public int Quantity { get; set; }

		// khóa ngoại
		public virtual List<ResourcePropertyDetail> ResourcePropertyDetails { get; set;}
		public virtual Resources Resources { get; set; }
    }
}
