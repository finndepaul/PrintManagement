using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrintManagement.API.Extensions;
using PrintManagement.Application.InterfaceServices;
using PrintManagement.Application.Payloads.RequestModels.TeamRequests;
using System.Reflection.Metadata;

namespace PrintManagement.API.Controllers
{
    [Route(Constants.DefaultValue.DEFAULT_CONTROLLER_ROUTE)]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            return Ok(await _teamService.GetAllTeam(cancellationToken));
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateTeamAsync(TeamCreateRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _teamService.CreateTeam(request, cancellationToken));
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateTeamAsync(TeamUpdateRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _teamService.UpdateTeam(request, cancellationToken));
        }
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteTeamAsync(TeamDeleteRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _teamService.DeleteTeam(request, cancellationToken));
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> SwapManagerAsync(Guid teamId, Guid managerId, CancellationToken cancellationToken)
        {
            return Ok(await _teamService.SwapManager(teamId, managerId, cancellationToken));
        }
    }
}
