using PrintManagement.Application.Payloads.RequestModels.TeamRequests;
using PrintManagement.Application.Payloads.ResponseModels;
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
		public async Task<DataResponseUser> EntityToDTOAsync(User user)
		{
			return new DataResponseUser
			{
				Id = user.Id,
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
