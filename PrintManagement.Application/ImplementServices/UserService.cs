using Microsoft.AspNetCore.Http;
using PrintManagement.Application.Handle.HandleEmail;
using PrintManagement.Application.InterfaceServices;
using PrintManagement.Application.Payloads.Mappers.Converters;
using PrintManagement.Application.Payloads.RequestModels.UserRequests;
using PrintManagement.Application.Payloads.ResponseModels.DataUsers;
using PrintManagement.Application.Payloads.Responses;
using PrintManagement.Domain.Entities;
using PrintManagement.Domain.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

namespace PrintManagement.Application.ImplementServices
{
	public class UserService : IUserService
	{
		private readonly IBaseRepository<User> _baseUserRepository;
		private readonly IBaseRepository<ConfirmEmail> _baseConfirmEmailRepository;
		private readonly IUserRepository _userRepository;
		private readonly IEmailService _emailService;
		private readonly UserConverter _userConverter;

		public UserService(IBaseRepository<User> baseUserRepository, IBaseRepository<ConfirmEmail> baseConfirmEmailRepository, UserConverter userConverter, IUserRepository userRepository, IEmailService emailService)
		{
			_baseUserRepository = baseUserRepository;
			_baseConfirmEmailRepository = baseConfirmEmailRepository;
			_userConverter = userConverter;
			_userRepository = userRepository;
			_emailService = emailService;
		}

		public async Task<ResponseObject<DataResponseUser>> ChangePassword(Guid userId, ChangePasswordRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var user = await _baseUserRepository.GetByIdAsync(x => x.Id == userId, cancellationToken); // không phải check nó có tồn tại hay không, vì đây là tài khoản đang trong phiên đăng nhập
				bool checkPass = BCryptNet.Verify(request.OldPassword, user.Password);
				if (!checkPass)
				{
					return new ResponseObject<DataResponseUser>
					{
						Status = StatusCodes.Status400BadRequest,
						Message = "Mật khẩu không chính xác",
						Data = null
					};
				}
				if (!request.NewPassword.Equals(request.ConfirmPassword))
				{
					return new ResponseObject<DataResponseUser>
					{
						Status = StatusCodes.Status400BadRequest,
						Message = "Mật khẩu mới không trùng khớp",
						Data = null
					};
				}
				user.Password = BCryptNet.HashPassword(request.NewPassword);
				user.UpdateTime = DateTime.Now;
				await _baseUserRepository.UpdateAsync(user, cancellationToken);
				return new ResponseObject<DataResponseUser>
				{
					Status = StatusCodes.Status200OK,
					Message = "Đổi mật khẩu thành công",
					Data = await _userConverter.EntityToDTOAsync(user, cancellationToken)
				};
			}
			catch (Exception ex)
			{
				return new ResponseObject<DataResponseUser>
				{
					Status = StatusCodes.Status500InternalServerError,
					Message = ex.Message,
					Data = null
				};
			}
		}

		public async Task<string> ConfirmCreateNewPassword(CreateNewPasswordRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var confirmEmail = await _baseConfirmEmailRepository.GetAsync(x => x.ConfirmCode.Equals(request.ConfirmCode.Trim()), cancellationToken);
				if (confirmEmail == null)
				{
					return "Mã xác nhận không hợp lệ";
				}
				if (confirmEmail.ExpiryTime < DateTime.Now)
				{
					return "Mã xác nhận đã hết hạn";
				}
				if (!request.NewPassword.Equals(request.ConfirmPassword))
				{
					return "Mật khẩu không trùng khớp";
				}
				var user = await _baseUserRepository.GetAsync(x => x.Id == confirmEmail.UserId, cancellationToken);
				user.Password = BCryptNet.HashPassword(request.NewPassword);
				user.UpdateTime = DateTime.Now;
				await _baseUserRepository.UpdateAsync(user, cancellationToken);
				return "Tạo mật khẩu mới thành công";
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}

		public async Task<string> ForgetPassword(string email, CancellationToken cancellationToken)
		{
			try

			{
				var user = await _userRepository.GetUserByEmailAsync(email, cancellationToken);
				if (user == null)
				{
					return "Email không tồn tại trong hệ thống";
				}
				ConfirmEmail confirmEmail = new ConfirmEmail
				{
					ConfirmCode = GenerateCodeActive(),
					ExpiryTime = DateTime.Now.AddMinutes(1),
					UserId = user.Id,
					IsConfirmed = false,
				};
				confirmEmail = await _baseConfirmEmailRepository.CreateAsync(confirmEmail, cancellationToken);
				var message = new EmailMessage(new string[] { user.Email }, "Nhận mã xác nhận tại đây: ", $"Mã xác nhận là: {confirmEmail.ConfirmCode}");
				var send = _emailService.SendEmail(message);

				return "Gửi mã xác nhận về email thành công! Vui lòng kiểm tra email";
			}
            catch (Exception ex)
            {
				return ex.Message;
			}
		}
		private string GenerateCodeActive()
		{
			string str = "DuyDong_" + DateTime.Now.Ticks.ToString();
			return str;
		}
	}
}
