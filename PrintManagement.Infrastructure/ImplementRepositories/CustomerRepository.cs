using Microsoft.EntityFrameworkCore;
using PrintManagement.Domain.Entities;
using PrintManagement.Domain.InterfaceRepositories;
using PrintManagement.Infrastructure.Database.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Infrastructure.ImplementRepositories
{
    public class CustomerRepository : ICustomerRepostory
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public Task<Customer> GetCustomerByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken)
        {
            var model = _context.Customers.SingleOrDefaultAsync(x => x.PhoneNumber == phoneNumber, cancellationToken);
            return model;
        }
    }
}
