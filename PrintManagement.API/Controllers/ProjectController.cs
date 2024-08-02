using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrintManagement.API.Extensions;
using PrintManagement.Application.InterfaceServices;
using PrintManagement.Application.Payloads.RequestModels.ProjectRequests;

namespace PrintManagement.API.Controllers
{
    [Route(Constants.DefaultValue.DEFAULT_CONTROLLER_ROUTE)]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateProjectAsync(ProjectCreateRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _projectService.CreateProject(request, cancellationToken));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProject(CancellationToken cancellationToken)
        {
            return Ok(await _projectService.GetAllProject(cancellationToken));
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetProjectById([FromQuery] Guid projectId, CancellationToken cancellationToken)
        {
            return Ok(await _projectService.GetProjectById(projectId, cancellationToken));
        }
        
    }
}
