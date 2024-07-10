using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrintManagement.API.Extensions;
using PrintManagement.Application.InterfaceServices;
using PrintManagement.Application.Payloads.RequestModels.UserRequests;

namespace PrintManagement.API.Controllers
{
	[Route(Constant.DefaultValue.DEFAULT_CONTROLLER_ROUTE)]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken)
		{
			return Ok(await _authService.Register(request, cancellationToken));
		}
		[HttpPost]
		public async Task<IActionResult> ConfirmRegisterAccount(string confirmCode, CancellationToken cancellationToken)
		{
			return Ok(await _authService.ConfirmRegisterAccount(confirmCode, cancellationToken));
		}
	}
}
