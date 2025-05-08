using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.Dto;

namespace Presentation
{
    //[Authorize]
     
    public class BasketController(IService_Manager _service_Manager) :ApiController  
    {
        [HttpGet("{id}")] //Get /api/Basket
        public async Task<ActionResult< BasketDTo>> GetAll(string id)
        { 
          var basket=await   _service_Manager.basketService.GetBasketAsync(id);
            if (basket is null)
                return NotFound();
            return Ok(basket);
        }


        [HttpPost] //Post /api/Basket 
        public async Task<ActionResult<BasketDTo>> UpdateBasket(BasketDTo basket)
        {  
            var updatedBasket = await _service_Manager.basketService.UpdateBasketAsync(basket); 
            return Ok(updatedBasket);
        }

        [HttpDelete("{id}")] //Delete /api/Basket
        public async Task<ActionResult<bool>> DeleteBasket(string id)
        { 
            var deleted = await _service_Manager.basketService.DeleteBasketAsync(id); 
            return NoContent();
        }
    }
}
