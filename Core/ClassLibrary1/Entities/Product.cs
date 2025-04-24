using Domain.Entities.CmmonEntities;

namespace Domain.Entities
{
    public class Product : BaseEntitiyID<int>
    {
        public required string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        //Navigational property [one]
        public ProductBrand ProductBrand { get; set; }
        //FR
        public int BrandId { get; set; }

        //Navigational property [one]
        public ProductType ProductType { get; set; }
        //FR
        public int TypeId { get; set; }
        
     }
}
