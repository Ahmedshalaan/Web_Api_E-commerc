using Domain;
using Domain.Entities.orderEntities;

namespace Services.Specifications
{
    internal class OrderWithpaymentIntentSpacification: absSpecifications<Order>
    {
        public OrderWithpaymentIntentSpacification(string paymentIntentId) : base(x => x.PaymentIntentId == paymentIntentId)
        {
            
        }
    } 
}
