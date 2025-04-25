namespace Domain.Entities
{
    public class BasketItems //product at the Cart
    {
        public int Id { get; set; } //product id
        public string ProductName { get; set; } //product name
        public string PictureUrl { get; set; } //product image
        public decimal Price { get; set; } //product price
        public int Quantity { get; set; } //product quantity
        public string Brand { get; set; } //product brand
        public string Category { get; set; } //product Catergory
    }
    
}
