namespace Shared.Dto
{
    public record BasketDTo
    {
        public string Id { get; init; } //بيتكون من  Key Value paire
        public IEnumerable<BasketItemDTo> Items { get; init; } = new List<BasketItemDTo>();
        public string? PaymentIntentId { get; set; }
        public string? ClintSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal? ShipingPrice { get; set; }
    }
}
