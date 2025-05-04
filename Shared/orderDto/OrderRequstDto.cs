namespace Shared.orderDto
{
    public record OrderRequstDto
    {
        public string BasketId { get; set; }
        public AddressDto ShipingAddress { get; set; }
        public int DeliveryMethodId { get; set; }
    }
}
