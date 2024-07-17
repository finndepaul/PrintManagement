using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.Payloads.RequestModels.UserRequests
{
	public class LoginRequest
	{
		[Required(ErrorMessage = "User Name field is required")]
		public string UserName { get; set; }
		[Required(ErrorMessage = "Password field is required")]
		public string Password { get; set; }
	}
}
