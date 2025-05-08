using Shared.orderDto;

namespace Services.Abstractions
{
    public interface IOrderService
    {
        //Get order by id  ==> orderResult(Guid Id)
        Task<OrderResultDto> GetOrderByIdAsync(Guid id);
        //get all orders for user Email  ==> Ienumrable<orderResult
        Task<IEnumerable<OrderResultDto>> GetOrdersForUserByEmailAsync(string userEmail);
        // Create order ==>orderResult (orderRequst ,string useEmail)
        Task<OrderResultDto> CreateOrderAsync(OrderRequstDto orderRequst, string userEmail);
        // Get All Delivery Methods ===>IEnumrable<DeliveryMethodResult>()
        Task<IEnumerable<DeliveryMethodResultDto>> GetAllDeliveryMethodsAsync();
    }
}
