using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PrintManagement.Application.Handle.HandleEmail;
using PrintManagement.Application.InterfaceServices;
using PrintManagement.Application.Payloads.Mappers.Converters;
using PrintManagement.Application.Payloads.RequestModels.UserRequests;
using PrintManagement.Application.Payloads.ResponseModels.DataUsers;
using PrintManagement.Application.Payloads.Responses;
using PrintManagement.Domain.Entities;
using PrintManagement.Domain.InterfaceRepositories;
using PrintManagement.Domain.Validations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt; // using tay

namespace PrintManagement.Application.ImplementServices
{
	public class AuthService : IAuthService
	{
		private readonly IConfiguration _configuration;
		private readonly UserConverter _userConverter;
		private readonly IBaseRepository<User> _baseUserRepository;
		private readonly IBaseRepository<ConfirmEmail> _baseConfirmEmailRepository;
		private readonly IBaseRepository<Permissions> _basePermissionsRepository;
		private readonly IBaseRepository<Role> _baseRoleRepository;
		private readonly IBaseRepository<RefreshToken> _baseRefreshTokenRepository;
		private readonly IUserRepository _userRepository;
		private readonly IEmailService _emailService;

		public AuthService(UserConverter userConverter, IBaseRepository<User> baseUserRepository, IBaseRepository<ConfirmEmail> baseConfirmEmailRepository, IUserRepository userRepository, IEmailService emailService, IBaseRepository<Permissions> basePermissionsRepository, IBaseRepository<Role> baseRoleRepository, IConfiguration configuration, IBaseRepository<RefreshToken> baseRefreshTokenRepository)
		{
			_configuration = configuration;
			_userConverter = userConverter;
			_baseUserRepository = baseUserRepository;
			_baseConfirmEmailRepository = baseConfirmEmailRepository;
			_userRepository = userRepository;
			_emailService = emailService;
			_basePermissionsRepository = basePermissionsRepository;
			_baseRoleRepository = baseRoleRepository;
			_baseRefreshTokenRepository = baseRefreshTokenRepository;
		}

		public async Task<ResponseObject<DataResponseUser>> Register(RegisterRequest request, CancellationToken cancellationToken)
		{
			try
			{
				if (!RegisterValidation.IsValidEmail(request.Email))
				{
					return new ResponseObject<DataResponseUser>
					{
						Status = StatusCodes.Status400BadRequest, // nuget ...http.Abstractions
						Message = "Định dạng email không hợp lệ",
						Data = null
					};
				}
				if (!RegisterValidation.IsValidPhoneNumber(request.PhoneNumber))
				{
					return new ResponseObject<DataResponseUser>
					{
						Status = StatusCodes.Status400BadRequest, 
						Message = "Định dạng SDT không hợp lệ",
						Data = null
					};
				}
				if (await _userRepository.GetUserByEmailAsync(request.Email,cancellationToken) != null)
				{
					return new ResponseObject<DataResponseUser>
					{
						Status = StatusCodes.Status400BadRequest, 
						Message = "Email đã tồn tại! Vui lòng sử dụng email khác",
						Data = null
					};
				}
				if (await _userRepository.GetUserByPhoneNumberAsync(request.PhoneNumber, cancellationToken) != null)
				{
					return new ResponseObject<DataResponseUser>
					{
						Status = StatusCodes.Status400BadRequest,
						Message = "SDT đã tồn tại! Vui lòng sử dụng Sdt khác",
						Data = null
					};
				}
				if (await _userRepository.GetUserByUsernameAsync(request.UserName, cancellationToken) != null)
				{
					return new ResponseObject<DataResponseUser>
					{
						Status = StatusCodes.Status400BadRequest,
						Message = "Username đã tồn tại! Vui lòng sử dụng username khác",
						Data = null
					};
				}				
				var user = new User
				{
					Avatar = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAMwAAADACAMAAAB/Pny7AAAAb1BMVEX///8uNDaHiYoiKiyhoqQAAAAnLjArMTMAExecnZ719fX7+/v4+PgfJinv7+9wc3QPGh17fX6PkZIaIiXg4eHb29xfY2Tm5+fU1dVNUVOqq6w6P0G6u7sVHiE0OTvJyspFSksACg9oa2xWWlsAAAlKDmu9AAAGYklEQVR4nO2d2XqjOBBGDTIS2GbfN4PB7/+MA/F4Ynd7wVJBVTKcu1zka/5Iqk2l6s1mZWVlZeV/RlQVuu+7ruvr2ybF/hoF4m1SHjWNc8Y441xoxzKxIuyvkiHVbTPkQgjtP4YfeGiK4Ict0KFpDe9Gxi3CM/LmgP2Fk3Gq0mCPlVxgRls52F85CafqTy+lfMkxk2qH/aXvyXQ7fCdlJLT1GPtb31Hkz87K32enL7C/9iWHQLOnSRmxtWCP/cXPSbuQT9cyeKDQJWsH4vyDZbkQJkTVSGgZtlpP0oVm7SQr9ic1RTVpUsto0TSP3rlxfE9Oy6BGJ+Y+91tpLZqoC1oWutE+ssn3sLLB/v5bnFzq8F+pE0obLTBVtGiaucVW8E1sTozHniE8Ohatl/CW99QutoYrmaGqRdMMKkuTK1iyK3aHreJCJun67/FoRDU+wMIMzibA1jHitCBieE4hDCg0Rbt8QRwphAHu21LMNLiOrWSz2SVAYhiBmCaDMMwjPM+wtWyKI8iRGQ8NfuUpANplFIzz3lcK/m8JfWzj7HTKQeYVG70YEEEZs0FMgn0RBWbMhjPTY5uzBk4Mz7FjgAYmMvsS065iVjFPxACeGXQxgNYMPziL4fwMS7AvOVPACKDDLgP8qtjsV0XNvyufgcs00S0zYA3AJlADAKvOMB9byUAFc2jEscJWMrADqmi2JHrQdJB9xgjUAAciELcZYufM/wIR0VC5n9lEEDdnRBZmWBrl6yY6d5qb9KR623zGDphvsM5qYgxCfQAbp1Uyz3aOXcu8I/MUPCf3sPPle/bF1KbZvyHX1bRxfFtSjbADUptsJJJ1ncwlZMmuZHIdNPi1/4fEMmrsHru+9ISo/zgSqHuS6zLidB9GaUZH7ux/cwiMD2yaMHQSCdlTqtPk7CY8UUiUX+J0IZ+wOoLyo4Ybqp6zN3IEEzn5ZbngWIl49faEh1qy/QnLciEt3NJ7HN8I2yv94udIGXEqq9PM+n6/CVabx876CU/n/iRqCj3RvPPJGzHNs6cletGQ9ZLvcKKsqQorCAJrW1RNFv2s7fWA/WG32x0OxHKWlZWVlZWVlZVfzX6/d+IhrCy2EymG0DN2hl/D/vJ7dk5UWW5rGoZxMqdzOo2/UPrbJnWIlGnSuNLbk2F+NqThNok2jboPhkXCVuJkhWufa9W2BsG889EvMkw9caG3ZgjUojWsUKsXWHloFiS28prc66lZYmEU0jM9r8HaM7+xwz5YWk6qtzboonzDWLvoVdphW7KZpIxwVi53yxnnKnfLk+R4C8082Vmv533BYBvFAqXCtAVo+5mCkcy9OIeGgTVlv6PWmllPjrOd+7TcwsI5C+ypLtu5IIew5xuGGLnvLpHA1TB/JjVRsrSWUc08/Rup+gwTGTX2HLfrO5npZRCE8G31h1x+spQiZgdtoTvFAUYqGMCvBIKF3P4TNaAva6oTppZhpwE+rcmOC/r9R/ASLGFL4Z4vysJcKJMWTGmGmRchgDqfmxJ5k40AvUhL3cWC/leEPkQkUKF5y3s8gDaoLEc//ReY+jvug3XCVnHlZKlW12OBbsmu8KOiszmoDpWExLTUIs5o2Tz5NYKpldUpLcx4alS07KDuK2AQtso+qxSfX0FzVvE1JREfc4W18lpS1JTsEQqDUAOQcZ+QePImgEK4fA9vZbVESMWlV9iyNcEtseM/wmRrGx1BMVzyETTQCAZYZEfUNiUp939BlHJZDdh4HEhkR+3AzMaAhssNQYKbjwWJLWUBHPzS3yNYIpM8xz1BYzZO25Rxm4CjCyGRG7ZX0YvMRuRKmyQt8+hoZBI0oEHs0Mg5GovkLhv+wjL3AUTFaFKXG1TFcJlkcxWzAKuYVcwCrGJWMfMj1xHwq2Kzimg+08pEzYDTyyGRS86chGZBQ67L0adZ0JBrCyRpAWSLgBHFIgBvJe80KN4CMNlRqA1BCxBKt53R8zQ8l9WyaejdNiu0afUkmgC/qVWmOjuyD5bngQulHtqKkhouf/q/2Ft0WoE4U+w32+wCKmq4HSgPDdgFc737/QzOA4Cm851FQQ1jW5AG+kMl0BuC6mMFNZgiTQzUxeGwY10b5mG9bhDc1KDHhle5JhhfVpHg4z/Yz/Gf7MRWl7elYEvBRdn27na2uQ1pU1mBvhCBVTXo41tWVlZWEPgHF9WLRrafYeAAAAAASUVORK5CYII=",
					IsActive = false,
					CreateTime = DateTime.Now,
					DateOfBirth = request.DateOfBirth,
					Email = request.Email,
					FullName = request.FullName,
					Password = BCryptNet.HashPassword(request.Password), // using tay
					PhoneNumber = request.PhoneNumber,
					UserName = request.UserName,
				};
				await _baseUserRepository.CreateAsync(user, cancellationToken);
				await _userRepository.AddRolesToUserAsync(user, new List<string> { "Employee" }, cancellationToken);

				// gửi Gmail
				ConfirmEmail confirmEmail = new ConfirmEmail()
				{
					ConfirmCode = GenerateCodeActive(),
					ExpiryTime = DateTime.Now.AddMinutes(1), // để trong 1 phút
					IsConfirmed = false,
					UserId = user.Id,
				};
				confirmEmail = await _baseConfirmEmailRepository.CreateAsync(confirmEmail, cancellationToken);
				var message = new EmailMessage(new string[] { request.Email }, "Nhận mã xác nhận tại đây: ", $"Mã xác nhận: {confirmEmail.ConfirmCode}");
				var reponseMessage = _emailService.SendEmail(message);

				return new ResponseObject<DataResponseUser>()
				{
					Status = StatusCodes.Status201Created,
					Message = "Bạn đã gửi yêu cầu đăng kí! Vui lòng nhận mã xác nhận tại email để đăng kí tài khoản <3",
					Data = await _userConverter.EntityToDTOAsync(user, cancellationToken)
				};

			}
			catch (Exception ex)
			{
				return new ResponseObject<DataResponseUser>
				{
					Status = StatusCodes.Status500InternalServerError,
					Message = "Error: " + ex.Message,
					Data = null
				};
			}
		}
		private string GenerateCodeActive()
		{
			string str = "DuyDong_" + DateTime.Now.Ticks.ToString();
			return str;
		}
		public async Task<string> ConfirmRegisterAccount(string confirmCode, CancellationToken cancellationToken)
		{
			try
			{
				var code = await _baseConfirmEmailRepository.GetAsync(x => x.ConfirmCode.Equals(confirmCode.Trim()), cancellationToken);
				if (code == null)
				{
					return "Mã xác nhận không hợp lệ";
				}
				var user = await _baseUserRepository.GetAsync(x => x.Id == code.UserId, cancellationToken);
				if (code.ExpiryTime < DateTime.Now)
				{
					return "Mã xác nhận đã hết hạn";
				}
				user.IsActive = true;
				code.IsConfirmed = true;
				await _baseUserRepository.UpdateAsync(user, cancellationToken);
				await _baseConfirmEmailRepository.UpdateAsync(code, cancellationToken);
				return "Xác nhận đăng ký tài khoản thành công, bạn có thể sử dụng tài khoản để đăng nhập";
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}
		private async Task<ResponseObject<DataResponseLogin>> GetJwtTokenAsync(User user, CancellationToken cancellationToken)
		{
			var permissions = await _basePermissionsRepository.GetAllAsync(x => x.UserId == user.Id, cancellationToken);
			var roles = await _baseRoleRepository.GetAllAsync(null, cancellationToken);

			var authClaims = new List<Claim>()
			{
				 new Claim("Id", user.Id.ToString()),
				 new Claim("UserName", user.UserName.ToString()),
				 new Claim("Email", user.Email.ToString()),
				 new Claim("PhoneNumber", user.PhoneNumber.ToString()),
			};
			foreach (var permission in permissions)
			{
				foreach (var role in roles)
				{
					if (role.Id == permission.RoleId)
					{
						authClaims.Add(new Claim("Permission", role.RoleName));
					}
				}
			}
			var userRoles = await _userRepository.GetRolesOfUserAsync(user, cancellationToken);
			foreach (var item in userRoles)
			{
				authClaims.Add(new Claim(ClaimTypes.Role, item));
			}
			var jwtToken = GetToken(authClaims);
			var refreshToken = GenerateRefreshToken();
			_ = int.TryParse(_configuration["JWT:RefreshTokenValidity"], out int refreshTokenValidity);
			RefreshToken rf = new RefreshToken
			{
				ExpiryTime = DateTime.Now.AddHours(refreshTokenValidity),
				UserId = user.Id,
				Token = refreshToken
			};
			rf = await _baseRefreshTokenRepository.CreateAsync(rf, cancellationToken);
			return new ResponseObject<DataResponseLogin>
			{
				Status = StatusCodes.Status200OK,
				Message = "Tạo token thành công",
				Data = new DataResponseLogin
				{
					AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken), // vì jwtToken nó là 3 phần của jwt (header - payload - verify signature) nên phải ép nó về kiểu chuỗi
					RefreshToken = refreshToken,
				}
			};
		}

		public async Task<ResponseObject<DataResponseLogin>> Login(LoginRequest request, CancellationToken cancellationToken)
		{
			var user = await _baseUserRepository.GetAsync(x => x.UserName.Equals(request.UserName), cancellationToken);
			if (user == null)
			{
				return new ResponseObject<DataResponseLogin>
				{
					Status = StatusCodes.Status400BadRequest,
					Message = "Sai tên tài khoản",
					Data = null
				};
			}
			if (user.IsActive == false)
			{
				return new ResponseObject<DataResponseLogin>
				{
					Status = StatusCodes.Status401Unauthorized,
					Message = "Tài khoản chưa được xác thực",
					Data = null
				};
			}
			bool checkPass = BCryptNet.Verify(request.Password, user.Password);
			if (!checkPass)
			{
				return new ResponseObject<DataResponseLogin>
				{
					Status = StatusCodes.Status400BadRequest,
					Message = "Mật khẩu không chính xác",
					Data = null
				};
			}
			return new ResponseObject<DataResponseLogin>
			{
				Status = StatusCodes.Status200OK,
				Message = "Đăng nhập thành công",
				Data = new DataResponseLogin
				{
					AccessToken = GetJwtTokenAsync(user, cancellationToken).Result.Data.AccessToken,
					RefreshToken = GetJwtTokenAsync(user, cancellationToken).Result.Data.RefreshToken,
				}
			};
		}
		
		#region Private Methods
		private JwtSecurityToken GetToken(List<Claim> authClaims)
		{
			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
			_ = int.TryParse(_configuration["JWT:TokenValidityInHours"], out int tokenValidityInHours);
			var expirationUTC = DateTime.Now.AddHours(tokenValidityInHours);

			var token = new JwtSecurityToken
				(
					issuer: _configuration["JWT:ValidIssuer"],
					audience: _configuration["JWT:ValidAudience"],
					expires: expirationUTC,
					claims: authClaims,
					signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
				);
			return token;
		}
		private string GenerateRefreshToken()
		{
			var randomNumber = new byte[64];
			var range = RandomNumberGenerator.Create();
			range.GetBytes(randomNumber);
			return Convert.ToBase64String(randomNumber);
		}
		#endregion
	}
}
