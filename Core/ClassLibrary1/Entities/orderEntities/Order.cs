using Domain.Entities.CmmonEntities;

namespace Domain.Entities.orderEntities
{
    public class Order :BaseEntitiyID<Guid>
    {
        public string UserEmail { get; set; }
        public OrderAddress  shippingAddress { get; set; }
        //order ==> Sandwich[orderItem] ==> Coffee [orderItem02] ==> Pizza [orderItem03]
        public List<OrderItem> OrderItems { get; set; } = [];
        public OrderPaymentStatus paymentStatus { get; set; } = OrderPaymentStatus.Pending; //By Default 
        public DeliveryMethod  deliveryMethod { get; set; }
        public int? DeliveryMethodID { get; set; }
        public decimal SubTotal { get; set; }//OrderItem.Price * OrderItem.Quantity /rtotar ==>Subtotal + ShippingPrice  
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public string PaymentIntentId    { get; set; } = string.Empty;

    }
}
