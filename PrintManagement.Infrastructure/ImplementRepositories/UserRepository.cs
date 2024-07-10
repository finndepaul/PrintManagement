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
	public class UserRepository : IUserRepository
	{
		private readonly AppDbContext _context;

		public UserRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task AddRolesToUserAsync(User user, List<string> lstRoles, CancellationToken cancellationToken)
		{
			if (user == null)
			{
				throw new ArgumentNullException(nameof(user));
			}
			if (lstRoles == null)
			{
				throw new ArgumentNullException(nameof(lstRoles));
			}
			foreach (var role in lstRoles)
			{
				var roleOfUser = await GetRolesOfUserAsync(user, cancellationToken); // check xem quyền có trùng
				if (await IsStringInListAsync(role,roleOfUser.ToList()))
				{
					throw new ArgumentException("Người dùng đã có quyền này rồi");
				}
				else
				{
					var roleItem = await _context.Roles.SingleOrDefaultAsync(x => x.RoleCode.Equals(role), cancellationToken);
					if (roleItem == null)
					{
						throw new ArgumentNullException("Không tồn tại quyền này");
					}
					_context.Permissions.Add(new Permissions
					{
						RoleId = roleItem.Id,
						UserId = user.Id,
					});
				}
			}
			await _context.SaveChangesAsync(cancellationToken);
		}

		public async Task<IEnumerable<string>> GetRolesOfUserAsync(User user, CancellationToken cancellationToken)
		{
			var lstRoles = new List<string>();
			var currentRoles = _context.Permissions.Where(x => x.UserId == user.Id).AsQueryable();
			foreach (var item in currentRoles.Distinct())
			{
				var role = await _context.Roles.SingleOrDefaultAsync(x => x.Id == item.RoleId, cancellationToken);
				lstRoles.Add(role.RoleCode);
			}
			return lstRoles.AsEnumerable();
		}	
		public async Task<Guid> GetTeamIdAsync(string teamName, CancellationToken cancellationToken)
		{
			var model = await _context.Teams.SingleOrDefaultAsync(x => x.Name.ToLower().Trim().Equals(teamName.ToLower().Trim()));
			return model.Id;
		}

		public async Task<string> GetTeamNameAsync(Guid teamId, CancellationToken cancellationToken)
		{
			var model = await _context.Teams.SingleOrDefaultAsync(x => x.Id == teamId, cancellationToken);
			return model.Name;
		}

		public async Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
		{
			var model = await _context.Users.SingleOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
			return model;
		}

		public async Task<User> GetUserByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken)
		{
			var model = await _context.Users.SingleOrDefaultAsync(x => x.PhoneNumber.ToLower().Equals(phoneNumber.ToLower()));
			return model;
		}

		public async Task<User> GetUserByUsernameAsync(string username, CancellationToken cancellationToken)
		{
			var model = await _context.Users.SingleOrDefaultAsync(x => x.UserName.ToLower().Equals(username.ToLower()));
			return model;
		}
		#region Xử lí chuỗi
		private Task<bool> CompareStringAsync(string str1, string str2)
		{
			if (string.IsNullOrEmpty(str1))
			{
				throw new ArgumentNullException(nameof(str1));
			}
			if (string.IsNullOrEmpty(str2))
			{
				throw new ArgumentNullException(nameof(str2));
			}
			return Task.FromResult(string.Equals(str1.ToLower(),str2.ToLower()));
		}
		private async Task<bool> IsStringInListAsync(string inputString, List<string> lstString)
		{
			if (string.IsNullOrEmpty(inputString))
			{
				throw new ArgumentNullException(nameof(inputString));
			}
			if (lstString == null)
			{
				throw new ArgumentNullException(nameof(lstString));
			}
			foreach (var item in lstString)
			{
				if (await CompareStringAsync(item,inputString))
				{
					return true;
				}
			}
			return false;
		}
		#endregion
	}
}
