using PrintManagement.Application.Payloads.RequestModels.UserRequests;
using PrintManagement.Application.Payloads.ResponseModels.DataUsers;
using PrintManagement.Application.Payloads.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.InterfaceServices
{
	public interface IUserService
	{
		Task<string> ForgetPassword(string email, CancellationToken cancellationToken);
		Task<string> ConfirmCreateNewPassword(CreateNewPasswordRequest request, CancellationToken cancellationToken);
		Task<ResponseObject<DataResponseUser>> ChangePassword(Guid userId, ChangePasswordRequest request, CancellationToken cancellationToken);
	}
}
