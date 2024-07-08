using PrintManagement.Domain.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Domain.Entities
{
	public class Delivery : BaseEntity
	{
        public int ShippingMethodId { get; set; }
        public int CustomerId { get; set; }
        public int DeliverId { get; set; }
        public int ProjectId { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime EstimateDeliveryTime { get; set; }
        public DateTime ActualDeliveryTime { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }

		// khóa ngoại
		public virtual ShippingMethod ShippingMethod { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Project Project { get; set; } // sửa lại thành no Action ở migration
	}
}
