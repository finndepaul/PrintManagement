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
	public interface IAuthService
	{
		Task<ResponseObject<DataResponseUser>> Register(RegisterRequest request, CancellationToken cancellationToken);
		Task<string> ConfirmRegisterAccount(string confirmCode, CancellationToken cancellationToken); 
	}
}
