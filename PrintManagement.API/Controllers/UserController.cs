﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrintManagement.API.Extensions;
using PrintManagement.Application.InterfaceServices;
using PrintManagement.Application.Payloads.RequestModels.UserRequests;
using PrintManagement.Application.Payloads.ResponseModels;
using PrintManagement.Domain.Entities;

namespace PrintManagement.API.Controllers
{
	[Route(Constants.DefaultValue.DEFAULT_CONTROLLER_ROUTE)]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet]
		public async Task<IActionResult> ForgetPassword([FromQuery] string email, CancellationToken cancellationToken)
		{
			return Ok(await _userService.ForgetPassword(email, cancellationToken));
		}
		[HttpPut]
		public async Task<IActionResult> ConfirmCreateNewPassword(CreateNewPasswordRequest request, CancellationToken cancellationToken)
		{
			return Ok(await _userService.ConfirmCreateNewPassword(request, cancellationToken));
		}
		[HttpPut]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> ChangePassword(ChangePasswordRequest request, CancellationToken cancellationToken)
		{
			Guid id = Guid.Parse(HttpContext.User.FindFirst("Id").Value);
			return Ok(await _userService.ChangePassword(id, request, cancellationToken));
		}
		[HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangeTeamForUser(Guid teamId, Guid userId, CancellationToken cancellationToken)
		{
			return Ok(await _userService.ChangeTeamForUser(teamId, userId, cancellationToken));
		}
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAllUser(CancellationToken cancellationToken)
        {
            return Ok(await _userService.GetAllUser(cancellationToken));
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUserById(Guid userId, CancellationToken cancellationToken)
        {
            return Ok(await _userService.GetUserById(userId, cancellationToken));
        }

    }
}
