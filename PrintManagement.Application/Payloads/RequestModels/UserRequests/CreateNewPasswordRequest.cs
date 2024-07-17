using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.Payloads.RequestModels.UserRequests
{
	public class CreateNewPasswordRequest
	{
		public string ConfirmCode {  get; set; }
		public string NewPassword { get; set; }
		public string ConfirmPassword { get; set; }
	}
}
