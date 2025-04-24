using Domain;
using Shared.Dto;

namespace Services.Specifications
{
    public class ProductCountSpecification : absSpecifications<Product>
    {
        public ProductCountSpecification(ProductSpecParams parameters) :
           base(p => // !BrandId.HasValue ==>  I Write this condition Becouse ==>> ||  this operation is a short time
                  (!parameters.BrandId.HasValue || p.BrandId == parameters.BrandId.Value)
                  &&
                   (!parameters.TypeId.HasValue || p.TypeId == parameters.TypeId.Value)
           &&
                      (string.IsNullOrWhiteSpace(parameters.Search) || p.Name.ToLower().Contains(parameters.Search.ToLower().Trim()))

           )

        {
           
           
        }
    }
}
