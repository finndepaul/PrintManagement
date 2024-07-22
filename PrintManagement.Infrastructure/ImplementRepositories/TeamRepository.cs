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
    public class TeamRepository : ITeamRepository
    {
        private readonly AppDbContext _context;

        public TeamRepository(AppDbContext context)
        {
            _context = context;
        }
        private async Task<bool> CheckRoleUser(Guid userId, CancellationToken cancellationToken)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(x => x.RoleCode.Equals("Manager"), cancellationToken);
            var permission = await _context.Permissions.FirstOrDefaultAsync(x => x.UserId == userId && x.RoleId == role.Id, cancellationToken);
            if(permission == null) return true;
            return false;
        }
        public async Task AddRoleForUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            var model = await CheckRoleUser(userId, cancellationToken);
            if (model)
            {
                var getRole = await _context.Roles.FirstOrDefaultAsync(x => x.RoleCode.Equals("Manager"), cancellationToken);
                var permission = await _context.Permissions.AddAsync(new Permissions
                {
                    RoleId = getRole.Id,
                    UserId = userId,
                }, cancellationToken);
                await _context.SaveChangesAsync();
            }
        }
        // vẫn còn bug
        //private async Task RemoveRoleOfUserAsync(CancellationToken cancellationToken)
        //{
        //    var getRole = await _context.Roles.FirstOrDefaultAsync(x => x.RoleCode.Equals("Manager"), cancellationToken);
        //    var permission = _context.Permissions.Where(x => x.RoleId == getRole.Id);
        //    foreach (var item in permission)
        //    {
        //        var manager = await _context.Teams.FirstOrDefaultAsync(x => x.ManagerId == item.UserId);
        //        if (manager == null)
        //        {
        //            _context.Permissions.Remove(item);
        //        }
        //    }
        //    await _context.SaveChangesAsync(cancellationToken);
        //}
        public async Task<string> GetManagerNameAsync(Guid id, CancellationToken cancellationToken)
        {
            var model = await _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            return model.FullName;
        }

        public async Task<int> GetNumberOfMemberAsync(Guid teamId, CancellationToken cancellationToken)
        {
            var model = await _context.Users.CountAsync(x => x.TeamId == teamId, cancellationToken);
            return model;
        }
    }
}
