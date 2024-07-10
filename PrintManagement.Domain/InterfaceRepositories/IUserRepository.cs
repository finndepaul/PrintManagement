using PrintManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Domain.InterfaceRepositories
{
	public interface IUserRepository
	{
		Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
		Task<User> GetUserByUsernameAsync(string username, CancellationToken cancellationToken);
		Task<User> GetUserByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken);
		Task AddRolesToUserAsync(User user, List<string>lstRoles, CancellationToken cancellationToken);
		Task<IEnumerable<string>> GetRolesOfUserAsync(User user, CancellationToken cancellationToken);
		Task<Guid> GetTeamIdAsync(string teamName, CancellationToken cancellationToken);
		Task<string> GetTeamNameAsync(Guid teamId, CancellationToken cancellationToken);
	}
}
