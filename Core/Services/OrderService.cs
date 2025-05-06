using Domain.Entities.orderEntities;
using Domain.Exceptions;
using Domain.Exceptions.NotFoundExcipitions;
using Shared.orderDto;
namespace Services
{
    internal class OrderService
        (Mapper _mapper,
        IBasketReposotory _basketReposotory,
        IUnitOfWork _unitOfWork
        ) : IOrderService
    {
        public async Task<OrderResultDto> CreateOrderAsync(OrderRequstDto orderRequst, string userEmail)
        {
            // 1. Address [Shipping Address]  
            var address = _mapper.Map<OrderAddress>(orderRequst.ShipingAddress); // Map OrderAddressDto to OrderAddress becouse Call Address in database


            // 2. OrderItems => Basket[BasketId] => BasketItems ==> OrderItems   
            var basket = await _basketReposotory.GetBasketAsync(orderRequst.BasketId)
                ?? throw new BasketNotFoundExcption(orderRequst.BasketId);

            //Get Item Add Basket ==> OrderItem
            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = await _unitOfWork.GetRepository<Product, int>().
                    GetByIdAsync(item.Id)
                    ??
                    throw new Product_Not_Found_Ex(item.Id);
                orderItems.Add(CreateOrder(item, productItem));
            }
            // 3. Delivery Methods
            var DelivaryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetByIdAsync(orderRequst.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundEx(orderRequst.DeliveryMethodId);
            // 4. SubTotal
            var subTotal = orderItems.Sum(x => x.Price * x.Quantity) + DelivaryMethod.Price;
            // 5. Create Order
            var order = new Order(userEmail, address, orderItems, DelivaryMethod, subTotal);
            // 6. Save To DB
            await _unitOfWork.GetRepository<Order, Guid>().AddAsync(order);
            await _unitOfWork.SaveChangesAsync();
            // Map<Order, OrderResult> & Return
            return _mapper.Map<OrderResultDto>(order);



        }

        private OrderItem CreateOrder(BasketItems item, Product productItem) => new OrderItem
            (new ProuductinOrderItem
            (productItem.Id
             , productItem.Name
             , productItem.PictureUrl),
            item.Quantity,
            item.Price);



        public async Task<IEnumerable<DeliveryMethodResultDto>> GetAllDeliveryMethodsAsync()   // IEnumerable<DeliveryMethodResultDto> =========>>Show Data
        {
            var methods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethodResultDto>>(methods);

        }

        public   async Task<OrderResultDto> GetOrderByIdAsync(Guid id)
        {
            //Get Order By ==>use Id ==> use Spacification Design Pattern
            var order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(new OrderWithIncludeSapcifications(id)) ?? throw new OrderNotFoundEx(id);
            // Map<Order, OrderResult> & Return
            return _mapper.Map<OrderResultDto>(order);
        }

        public async Task<IEnumerable<OrderResultDto>> GetOrdersForUserByEmailAsync(string userEmail)
        {
            var orders =await _unitOfWork.GetRepository<Order, Guid>()
                //Get Order By ==>use Email ==> use Spacification Design Pattern 
                .GetAllAsync(new OrderWithIncludeSapcifications(userEmail));
            return _mapper.Map<IEnumerable<OrderResultDto>>(orders);
        }
    }
}
