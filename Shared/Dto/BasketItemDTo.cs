using System.ComponentModel.DataAnnotations;

namespace Shared.Dto
{
    public class BasketItemDTo
    {
        public int Id { get; init; } //product id
        public string ProductName { get; init; } //product name
        public string PictureUrl { get; init; } //product image
        [Range(1,double.MaxValue, ErrorMessage = "Price Must Be Greater Then Zero!")]
        public decimal Price { get; init; } //product price

        [Range(1, 99,ErrorMessage = "Quantity Must Be At Least one Item And Max Value 99!")]
        public int Quantity { get; init; } //product quantity
        public string Brand { get; init; } //product brand
        public string Category { get; init; } //product Catergory
    }
}
