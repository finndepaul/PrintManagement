using PrintManagement.Domain.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Domain.Entities
{
	public class Resources : BaseEntity
	{
        public string ResourceName { get; set; }
        public string Ịmage { get; set; }
        public ResourceType ResourceType { get; set; }
        public int AvailableQuantity { get; set; }
        public ResourceStatus ResourceStatus { get; set; } // sẵn sàng sử dụng, cần bảo trì

		// khóa ngoại
		public virtual List<ResourceProperty> ResourceProperties { get; set; }
	}
}
