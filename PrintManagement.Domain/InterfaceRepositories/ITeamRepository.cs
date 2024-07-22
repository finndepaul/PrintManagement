using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Domain.InterfaceRepositories
{
    public interface ITeamRepository
    {
        Task<string> GetManagerNameAsync(Guid teamId, CancellationToken cancellationToken);
        Task<int> GetNumberOfMemberAsync(Guid teamId, CancellationToken cancellationToken);
        Task AddRoleForUserAsync(Guid userId, CancellationToken cancellationToken);
    }
}
