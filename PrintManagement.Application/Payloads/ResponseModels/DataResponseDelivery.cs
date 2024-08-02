using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.Payloads.ResponseModels
{
    public class DataResponseDelivery
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
    }
    public class DataResponseDeliveryComparer : IEqualityComparer<DataResponseDelivery>
    {
        public bool Equals(DataResponseDelivery x, DataResponseDelivery y)
        {
            return x.Id == y.Id; // So sánh theo Id
        }

        public int GetHashCode(DataResponseDelivery obj)
        {
            return obj.Id.GetHashCode(); // Trả về mã hash dựa trên Id
        }
    }
}
