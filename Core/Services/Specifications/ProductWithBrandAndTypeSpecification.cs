using Domain;
using Domain.Entities;
using Shared.Dto;

namespace Services.Specifications
{
    internal class ProductWithBrandAndTypeSpecification : absSpecifications<Product>
    {
        // This CTOR Wi: Be Used For Creating An Object , That Will Be Use To Get Product By Id
        public ProductWithBrandAndTypeSpecification(int id) : base(x => x.Id == id)//where ==> Crateria
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
        }
        // This CTOR Wi: Be Used For Creating An Object , That Will Be Use To Get Product 

        public ProductWithBrandAndTypeSpecification(ProductSpecParams parameters) :
           base(p => // !BrandId.HasValue ==>  I Write this condition Becouse ==>> ||  this operation is a short time
                  (!parameters.BrandId.HasValue || p.BrandId == parameters.BrandId.Value)
                  &&
                   (!parameters. TypeId.HasValue || p.TypeId == parameters.TypeId.Value)
           &&
           (string.IsNullOrWhiteSpace(parameters.Search)||p.Name.ToLower().Contains(parameters.Search.ToLower().Trim()))
           )

        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
            // Apply sorting
            if (!string.IsNullOrWhiteSpace(parameters.Sort))
            {
                switch (parameters.Sort.ToLower().Trim())
                {
                    case "priceasc":
                        SetOrderByasce(p => p.Price);
                        break;
                    case "pricedesc":
                        SetOrderByDesce(p => p.Price);
                        break;
                    case "namedesc":
                        SetOrderByasce(p => p.Name);
                        break;
                    default:
                        SetOrderByDesce(p => p.Name);
                        break;
                }
            }
            else
            {
                // Default sorting if no sort parameter is provided
                SetOrderByasce(p => p.Name);
            }
            ApplyPagination(parameters.Pageindex, parameters.Pagesize);
        }
    }
}
//Sort ,OrderBy ==>Collection


