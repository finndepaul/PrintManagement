using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrintManagement.API.Extensions;
using PrintManagement.Application.InterfaceServices;
using PrintManagement.Application.Payloads.RequestModels.DeliveryRequests;
using PrintManagement.Application.Payloads.ResponseModels;

namespace PrintManagement.API.Controllers
{
    [Route(Constants.DefaultValue.DEFAULT_CONTROLLER_ROUTE)]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;

        public DeliveryController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetDelivererForBill(Guid projectId, CancellationToken cancellationToken)
        {
            return Ok(await _deliveryService.GetDelivererForBill(projectId, cancellationToken));
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeliveryForEmployee(DeliveryForEmployeeRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _deliveryService.DeliveryForEmployee(request, cancellationToken));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBill(CancellationToken cancellationToken)
        {
            Guid userId = Guid.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _deliveryService.GetAllBill(userId, cancellationToken));
        }
        [HttpPut]
        public async Task<IActionResult> AcceptBill(Guid billId, bool IsAccept, CancellationToken cancellationToken)
        {
            return Ok(await _deliveryService.AcceptBill(billId, IsAccept, cancellationToken));
        }
        [HttpPut]
        public async Task<IActionResult> ConfirmBill(Guid billId, DeliveryConfirmRequest deliveryStatus, CancellationToken cancellationToken)
        {
            await _deliveryService.ConfirmBill(billId, deliveryStatus, cancellationToken);
            return Ok();
        }
    }
}
