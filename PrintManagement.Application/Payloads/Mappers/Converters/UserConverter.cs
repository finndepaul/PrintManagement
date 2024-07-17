using PrintManagement.Application.Payloads.ResponseModels.DataUsers;
using PrintManagement.Domain.Entities;
using PrintManagement.Domain.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.Payloads.Mappers.Converters
{
	public class UserConverter
	{
		private readonly IUserRepository _userRepository;

		public UserConverter(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<DataResponseUser> EntityToDTOAsync(User user, CancellationToken cancellationToken)
		{
			return new DataResponseUser
			{
				Avatar = user.Avatar,
				DateOfBirth = user.DateOfBirth,
				Email = user.Email,
				FullName = user.FullName,
				PhoneNumber = user.PhoneNumber,
				CreateTime = user.CreateTime,
				UpdateTIme = user.UpdateTime,
				IsActive = user.IsActive,
			};
		}
	}
}
