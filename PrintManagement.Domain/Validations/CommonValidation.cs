﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PrintManagement.Domain.Validations
{
	public class CommonValidation
	{
		public static bool IsValidEmail(string email)
		{
			var emailAtribute = new EmailAddressAttribute();
			return emailAtribute.IsValid(email);
		}
		public static bool IsValidPhoneNumber(string phoneNumber)
		{
			string pattern = @"^(84|0[35789])[0-9]{8}$";
			return Regex.IsMatch(phoneNumber, pattern);
		}
	}
}
