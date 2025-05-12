global using Product = Domain.Entities.Product;
using Domain.Entities.orderEntities;
using Domain.Exceptions.NotFoundExcipitions;
using Microsoft.Extensions.Configuration;
using Services.Specifications;
using Shared.Dto;
using Stripe;
using Stripe.Forwarding;

namespace Services
{
    internal class PayementService(
        IBasketReposotory _BasketRepository,
        IConfiguration _Configuration,
         IUnitOfWork _UnitOfWork,
        IMapper _Mapper)
       : IpaymentService 
        
    {
        // install package Stripe.net in ==>>Service
        //[=========================================]
        //1. Set up Stripe Api  Keyn[Security]
        //2.Get Basket => > BasketRepository
        //3.validate on Basket .item. Price = Product.Price[Db] [update Item Price ,Get => Product.price from Database]
        //4. Get Deliverymethod  and  Shipping price
        //5. Reteive  DelviryMethod from  Db and Assign price to  basket [Shipping price == DelviryMethod.Shippingprice ]
        //6. Total price = Basket.Items. (i => i.Price) *(Item.Quantity) + DeliveryMethod.ShippingPrice
        //7. Create or update PaymentIntent
        //8.Save All Changes to  Db 
        //9.Mapping Basket =======> BasketDTo & return it
        public async Task<BasketDTo> CreateOrUpdatePaymentAsync(string BasketId)
        {
            //c=Configuring  Stripe API Key Using the Sectrt Key from => appsettings.json
            //                                           ["StripSettings:SecrtKey"];
            StripeConfiguration.ApiKey = _Configuration.GetSection("StripSettings")["SecrtKey"];

            //Retrive Basket  By Id
            var basket = await _BasketRepository.GetBasketAsync(BasketId) 
                              ?? throw new BasketNotFoundExcption(BasketId);

            foreach (var item in basket.Items)  // one to many
            {
                //Get Product Price from Db
                var product = await _UnitOfWork.GetRepository<Product, int>()
                                     .GetByIdAsync(item.Id) 
                                      ?? throw new Product_Not_Found_Ex(item.Id);
                //Update Item Price
                item.Price = product.Price;
            }
            //Chack on DeliveryMethod
            if (!basket.DeliveryMethodId.HasValue)
                throw new Exception("No Delivary Method Was Select :(");

            //Get DeliveryMethod from Db
            var deliveryMethod = await _UnitOfWork.GetRepository<DeliveryMethod, int>()
                    .GetByIdAsync(basket.DeliveryMethodId.Value) 
                    ??
                    throw new DeliveryMethodNotFoundEx(basket.DeliveryMethodId.Value);

            //Assign Shipping Price to Basket for Database
            basket.ShipingPrice = deliveryMethod.Price;
            // Calculate Total Price = Acctule Price At DB 
            var amount = (long)(basket.Items.Sum(i => i.Price * i.Quantity) + basket.ShipingPrice) * 100;  //casting


            var service = new PaymentIntentService();
            // IF you want To Create or update PaymentIntent 
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                //Create PaymentIntent
                var CreateOption = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "USD",
                    PaymentMethodTypes = ["card"],  //list

                };
                var PaymentIntent = await service.CreateAsync(CreateOption);
                basket.PaymentIntentId = PaymentIntent.Id;
                basket.ClintSecret = PaymentIntent.ClientSecret;
            }
            else
            {
                //Update PaymentIntent

                // Update
                // 1.Procut Price Changed[Admin] 
                // 2. User Changes Delivery Method 
                // 3. Remove Any Item From Basket
                var updateOptions = new PaymentIntentUpdateOptions
                {
                    Amount = amount
                };
                await service.UpdateAsync(basket.PaymentIntentId, updateOptions);

            }

            await _BasketRepository. CreateOrUpdateBasketAsync(basket);
            return _Mapper.Map<BasketDTo>(basket);
        }

        public async Task UpdateOrderPaymentStuts(string requst, string header)
        {
            var endpointSecret = _Configuration.GetSection("StripSettings")["endpointSecret"];

            var stripeEvent = EventUtility.ConstructEvent(
                    requst,
                   header,
                    endpointSecret // Make sure this is defined (from your Stripe settings)
                    ,throwOnApiVersionMismatch: false
                );
            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
            // Handle the event
            switch (stripeEvent.Type)
            {
                case EventTypes.PaymentIntentSucceeded:
                    { 
                        await UpdatePaymentSucceeded(paymentIntent!.Id);
                        break;
                    }

                case EventTypes.PaymentIntentPaymentFailed:
                    {
                        await UpdatepaymentFeiald(paymentIntent!.Id);

                        break;
                    }

                default:
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                    break;
            }



        }
        #region  Successeded_or_Failed_PaymentStatus()

        private async Task UpdatePaymentSucceeded(string paymentIntentId)
        {
            var orderRepo = _UnitOfWork.GetRepository<Order, Guid>();

            var order = await orderRepo.GetByIdAsync(new OrderWithpaymentIntentSpacification(paymentIntentId))
                         ?? throw new Exception("Order not found for PaymentIntentId: " + paymentIntentId);

            order.paymentStatus = OrderPaymentStatus.PaymentPaid; // Correct status for success
            orderRepo.Update(order);

            await _UnitOfWork.SaveChangesAsync();
        }

        private async Task UpdatepaymentFeiald(string paymentIntentId)
        {
            var orderRepo = _UnitOfWork.GetRepository<Order, Guid>();

            var order = await orderRepo.GetByIdAsync(new OrderWithpaymentIntentSpacification(paymentIntentId))
                         ?? throw new Exception("Order not found for PaymentIntentId: " + paymentIntentId);

            order.paymentStatus = OrderPaymentStatus.PaymentFailed; // Correct status for success
            orderRepo.Update(order);

            await _UnitOfWork.SaveChangesAsync();
        }
        #endregion
    }
}
