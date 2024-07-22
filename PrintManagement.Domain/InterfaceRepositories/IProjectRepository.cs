using PrintManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Domain.InterfaceRepositories
{
    public interface IProjectRepository
    {
        Task<string> GetLeaderNameAsync(Guid employeeId, CancellationToken cancellationToken);
        Task<string> GetCustomerNameAsync(Guid customerId, CancellationToken cancellationToken);
        Task<bool> CheckLeaderAsync(Guid employeeId, CancellationToken cancellationToken);
        Task<bool> CheckTeamOfLeaderAsync(Guid employeeId, CancellationToken cancellationToken);
        Task AddRolesToUserAsync(Guid employeeId, CancellationToken cancellationToken);
    }
}
