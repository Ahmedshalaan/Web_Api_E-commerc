namespace Shared.orderDto
{
    public record DeliveryMethodResultDto
    {
        public int Id { get; init; }
        public string ShortName { get; init; }
        public string DeliveryTime { get; init; }
        public string Description { get; init; }
        public decimal Price { get; init; }
    }
}
