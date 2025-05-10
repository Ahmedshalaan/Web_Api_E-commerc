using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.Dto;

namespace Presentation
{
    public class PaymentsController(IService_Manager service_Manager) :ApiController
    {
        //Dinamic Segmant
        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketDTo>> CreateOrUpdatePayment(string basketId)
        {
            //Get BasketId from Route
            var basket = await service_Manager.ipaymentService.CreateOrUpdatePaymentAsync(basketId);
            return Ok(basket);
        }

    }
}
