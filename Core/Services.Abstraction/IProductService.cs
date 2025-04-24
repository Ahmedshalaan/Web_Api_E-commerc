using Shared;
using Shared.Dto;
namespace Services.Abstractions
{
    public interface IProductService
    {
        //Get All Products
        public Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync( ProductSpecParams parameters);

        //Get All Brands
        public Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
        //Get All Types
        public Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();
        //Get All Product By Id
        public Task<ProductResultDto> GetProductByIdAsync(int id);


    }
}
