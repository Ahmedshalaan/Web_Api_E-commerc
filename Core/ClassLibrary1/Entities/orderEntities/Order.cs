using Domain.Entities.CmmonEntities;

namespace Domain.Entities.orderEntities
{
    public class Order :BaseEntitiyID<Guid>
    {
        public Order(string paymentIntentId)
        {
            PaymentIntentId = paymentIntentId;
        }

        public Order(string userEmail,
            OrderAddress shippingAddress,
            ICollection<OrderItem> orderItems,
             DeliveryMethod deliveryMethod,
             decimal subTotal
,
             string paymentIntentId)
        {
            UserEmail = userEmail;
            this.shippingAddress = shippingAddress;
            OrderItems = orderItems;
            this.deliveryMethod = deliveryMethod;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }
        public string UserEmail { get; set; }
        public OrderAddress  shippingAddress { get; set; }
        //order ==> Sandwich[orderItem] ==> Coffee [orderItem02] ==> Pizza [orderItem03]
        public ICollection<OrderItem> OrderItems { get; set; } = [];
        public OrderPaymentStatus paymentStatus { get; set; } = OrderPaymentStatus.Pending; //By Default 
        public DeliveryMethod  deliveryMethod { get; set; }
        public int? DeliveryMethodID { get; set; }
        public decimal SubTotal { get; set; }//OrderItem.Price * OrderItem.Quantity /rtotar ==>Subtotal + ShippingPrice  
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public string PaymentIntentId    { get; set; } 

    }
}
