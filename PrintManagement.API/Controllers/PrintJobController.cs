using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrintManagement.API.Extensions;
using PrintManagement.Application.InterfaceServices;
using PrintManagement.Application.Payloads.RequestModels.ResourseForPrintjobRequest;
using PrintManagement.Domain.Entities;

namespace PrintManagement.API.Controllers
{
    [Route(Constants.DefaultValue.DEFAULT_CONTROLLER_ROUTE)]
    [ApiController]
    public class PrintJobController : ControllerBase
    {
        private readonly IResourseForPrintjobService _ser;

        public PrintJobController(IResourseForPrintjobService ser)
        {
            _ser = ser;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddResourseForPrintJob(ResourseForPrintjobCreateRequest request, CancellationToken cancellationToken)
        {
            await _ser.AddResourseForPrintJob(request, cancellationToken);
            return Ok();
        }
    }
}
