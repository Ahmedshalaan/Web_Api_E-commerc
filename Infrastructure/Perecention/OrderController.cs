using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.orderDto;
using System.Security.Claims;

namespace Presentation
{
    public class OrderController(IService_Manager service_Manager) :ApiController
    {
        // Create Order
        [HttpPost] //Post /api/Order
        public async Task<ActionResult<OrderResultDto>> CreateOrder(OrderRequstDto orderRequst)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

             var order = await service_Manager.orderService.CreateOrderAsync(orderRequst, userEmail);
            return Ok(order);
        }
        [HttpGet]
        public async Task<ActionResult<OrderResultDto>> GetOrdersForUser()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
             var orders = await service_Manager.orderService.GetOrdersForUserByEmailAsync(userEmail);
            return Ok(orders);
        }
        // Get Order By Id
        //              Dinamic
        [HttpGet("Orders/{id}")] //Get /api/Orders/1
        public async Task<ActionResult<OrderResultDto>> GetOrderById(Guid Id)
        {
            var order =await service_Manager.orderService.GetOrderByIdAsync(Id);

            return Ok(order);
        }

        //Get All Delivery Method
        [AllowAnonymous]
        //          Static Segmant
        [HttpGet("DeliveryMethods")] //Get /api/Orders/DeliveryMethods
        public async Task<ActionResult<IReadOnlyList<DeliveryMethodResultDto>>> GetDeliveryMethods()
        {
            var deliveryMethods = await service_Manager.orderService.GetAllDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }



    }
}
