namespace Domain.Entities
{
    public class CustomerBasket
    {
        //Cart ===> Id ,items :Product
        public string Id { get; set; } //بيتكون من  Key Value paire
            public IEnumerable<BasketItems> Items { get; set; } = new List<BasketItems>();

    }
}
