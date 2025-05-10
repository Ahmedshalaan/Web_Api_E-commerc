namespace Domain.Entities
{
    public class CustomerBasket
    {
        //Cart ===> Id ,items :Product
        public string Id { get; set; } //بيتكون من  Key Value paire
        public IEnumerable<BasketItems> Items { get; set; }
        public string?  PaymentIntentId { get; set; }
        public string? ClintSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal? ShipingPrice { get; set; }

    }
}
