using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrintManagement.API.Extensions;
using PrintManagement.Application.InterfaceServices;
using PrintManagement.Application.Payloads.RequestModels.DesignRequests;
using PrintManagement.Domain.Entities;
using PrintManagement.Domain.Enumerates;

namespace PrintManagement.API.Controllers
{
    [Route(Constants.DefaultValue.DEFAULT_CONTROLLER_ROUTE)]
    [ApiController]
    public class DesignController : ControllerBase
    {
        private readonly IDesignService _designService;

        public DesignController(IDesignService designService)
        {
            _designService = designService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDesign(CancellationToken cancellationToken)
        {
            return Ok(await _designService.GetAllDesign(cancellationToken));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UploadFile([FromForm] UploadFileRequest request, CancellationToken cancellationToken)
        {
            Guid designerId = Guid.Parse(HttpContext.User.FindFirst("Id").Value); 
            return Ok(await  _designService.UploadFile(request, designerId, cancellationToken));
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ApproveFile(Guid projectId, ApproveFileRequest status, CancellationToken cancellationToken)
        {
            Guid approverId = Guid.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _designService.ApproveFile(projectId, approverId, status, cancellationToken));
        }

        
    }
}
