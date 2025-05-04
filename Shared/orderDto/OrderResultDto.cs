namespace Shared.orderDto
{
    public record OrderResultDto
    {
        public Guid Id { get; set; }
        public string UserEmail { get; init; }
        public AddressDto shippingAddress { get; init; }
        //order ==> Sandwich[orderItem] ==> Coffee [orderItem02] ==> Pizza [orderItem03]
        public ICollection<OrderItemDto> OrderItems { get; init; } = [];
        public string paymentStatus { get; init; }  
        public string deliveryMethod { get; init; }
         public decimal SubTotal { get; init; }//OrderItem.Price * OrderItem.Quantity /rtotar ==>Subtotal + ShippingPrice  
        public DateTimeOffset OrderDate { get; init; } = DateTimeOffset.Now;
        public string PaymentIntentId { get; init; } = string.Empty;
        public decimal   Total { get; init; }

    }
}
