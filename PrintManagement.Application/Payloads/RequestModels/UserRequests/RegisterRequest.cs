using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.Payloads.RequestModels.UserRequests
{
	public class RegisterRequest
	{
		[Required(ErrorMessage = "User Name field is required")]
		public string UserName { get; set; }
		[Required(ErrorMessage = "Password field is required")]
		public string Password { get; set; }
		[Required(ErrorMessage = "Full Name field is required")]
		public string FullName { get; set; }
		[Required(ErrorMessage = "Date Of Birth field is required")]
		public DateTime DateOfBirth { get; set; }
		[Required(ErrorMessage = "Email field is required")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Phone Number field is required")]
		public string PhoneNumber { get; set; }
		[Required(ErrorMessage = "Team Id field is required")]
		public string TeamName { get; set; }
	}
}
