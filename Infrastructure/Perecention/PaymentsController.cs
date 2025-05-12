using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Abstractions;
using Shared.Dto;
using System.IO;

namespace Presentation
{
    public class PaymentsController(IService_Manager service_Manager) : ApiController
    {
        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketDTo>> CreateOrUpdatePayment(string basketId)
        {
            var basket = await service_Manager.ipaymentService.CreateOrUpdatePaymentAsync(basketId);
            return Ok(basket);
        }

        [HttpPost("webhook")] // https://localhost:7259/Api/payments/webhook 
        public async Task<IActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var stripeHeader = Request.Headers["Stripe-Signature"];

            await service_Manager.ipaymentService.UpdateOrderPaymentStuts(json, stripeHeader!);
            return new EmptyResult();
        }
    }
}
