using PrintManagement.Application.Payloads.ResponseModels;
using PrintManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.Payloads.Mappers.Converters
{
    public class CustomerConverter
    {
        public async Task<DataResponseCustomer> EntityToDTOAsync(Customer customer)
        {
            return new DataResponseCustomer
            {
                Id = customer.Id,
                FullName = customer.FullName,
                Address = customer.Address,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
            };
        }
    }
}
