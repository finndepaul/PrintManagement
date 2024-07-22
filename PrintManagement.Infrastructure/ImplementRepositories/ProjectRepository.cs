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
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }
        private async Task<bool> CheckRoleOfLeaderAsync(Guid employeeId, CancellationToken cancellationToken)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(x => x.RoleCode.Equals("Leader"), cancellationToken);
            var permission = await _context.Permissions.FirstOrDefaultAsync(x => x.UserId == employeeId && x.RoleId == role.Id, cancellationToken);
            if (permission == null) return true;
            return false;
        }
        public async Task AddRolesToUserAsync(Guid employeeId, CancellationToken cancellationToken)
        {
            var model = await CheckRoleOfLeaderAsync(employeeId, cancellationToken);
            if (model)
            {
                var role = await _context.Roles.FirstOrDefaultAsync(x => x.RoleCode.Equals("Leader"), cancellationToken);
                var permisson = await _context.Permissions.AddAsync(new Permissions
                {
                    UserId = employeeId,
                    RoleId = role.Id
                }, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        public async Task<bool> CheckLeaderAsync(Guid employeeId, CancellationToken cancellationToken)
        {
            var currentUser = await _context.Users.SingleOrDefaultAsync(x => x.Id == employeeId, cancellationToken);
            if (currentUser == null)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> CheckTeamOfLeaderAsync(Guid employeeId, CancellationToken cancellationToken)
        {
            var currentUser = await _context.Users.SingleOrDefaultAsync(x => x.Id == employeeId, cancellationToken);
            Guid teamId = (Guid)currentUser.TeamId;
            var model = await _context.Teams.FirstOrDefaultAsync(x => x.Id == teamId, cancellationToken);
            if (model == null || !model.Name.Equals("Technical"))
            {
                return false;
            }
            return true;
        }

        public async Task<string> GetCustomerNameAsync(Guid customerId, CancellationToken cancellationToken)
        {
            var model = await _context.Customers.FirstOrDefaultAsync(x => x.Id == customerId, cancellationToken);
            return model.FullName;
        }

        public async Task<string> GetLeaderNameAsync(Guid employeeId, CancellationToken cancellationToken)
        {
            var model = await _context.Users.FirstOrDefaultAsync(x => x.Id == employeeId, cancellationToken);
            return model.FullName;
        }
        
    }
}
