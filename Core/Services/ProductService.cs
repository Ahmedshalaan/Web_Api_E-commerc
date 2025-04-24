global using Services.Abstractions;
global using Shared;
global using AutoMapper;
global using Domain.Contracts;
global using Domain.Entities;
global using Services.Specifications;
using Shared.Dto;
using Domain.Exceptions;

namespace Services
{
    //Primary Constructor
    internal class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {


        public async Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductSpecParams parameters)
        {
            //1. Retriveing All Prduct =>UintOfWork
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(new ProductWithBrandAndTypeSpecification(parameters));
            //2. Mapping To ProductResultDto=>Imapper

            #region Manual Mapping
            ///var productResultDtos = products.Select(product => new ProductResultDto
            ///{
            ///    Id = product.Id,
            ///    Name = product.Name,
            ///    Description = product.Description,
            ///    Price = product.Price,
            ///    PictureUrl = product.PictureUrl, 
            ///    BrandName = product.ProductBrand?.Name,
            ///    TypeName = product.ProductType?.Name
            ///    // Map other properties as needed
            ///}).ToList(); 
            #endregion

            var productResultDtos = _mapper.Map<IEnumerable<ProductResultDto>>(products); //كده انا عايز اقولوا انا عايز احول لى ايه ع طول 

            // 3. Return Product
            var count = productResultDtos.Count(); //page size

            var totalCount = await _unitOfWork.GetRepository<Product, int>().CountAsync(new ProductCountSpecification(parameters));

            var result = new PaginatedResult<ProductResultDto>(
                 parameters.Pageindex,
                 count, //page size
                 totalCount,
                 productResultDtos //Data
             );

            return result;

        }

        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            //1. Retriveing  productId =>UintOfWork
            var productId = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(new ProductWithBrandAndTypeSpecification(id));


            ///2. Mapping To ProductResultDto=>Imapper 
            #region Manual Mapping
            ///if(productId is null) return null; // Handle not found case
            ///var productResultDto = new ProductResultDto
            ///{
            ///    Id = productId.Id,
            ///    Name = productId.Name,
            ///    Description = productId.Description,
            ///    Price = productId.Price,
            ///    PictureUrl = productId.PictureUrl, 
            ///    BrandName = productId.ProductBrand?.Name??string.Empty,
            ///    TypeName = productId.ProductType?.Name ?? string.Empty
            ///    // Map other properties as needed
            ///};

            #endregion

            //var productResultDto = _mapper.Map<ProductResultDto>(productId); //كده انا عايز اقولوا انا عايز احول لى ايه ع طول 
            /////3. Return Product
            //return productResultDto;
        return productId is null ? throw new Product_Not_Found_Ex(id) : _mapper.Map<ProductResultDto>(productId);
        }

        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            //1. Retriveing  ProductBrand =>UintOfWork
            var Brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            //2. Mapping To BrandResultDtos=>Imapper
            #region Manual Mapping
            //var BrandResultDtos = Brands.Select(Brands => new BrandResultDto
            //{
            //    Id = Brands.Id,
            //    Name = Brands.Name,
            //}).ToList(); 
            #endregion

            var BrandResultDtos = _mapper.Map<IEnumerable<BrandResultDto>>(Brands);
            //3. Return BrandResultDtos
            return BrandResultDtos;
        }


        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            //1. Retriveing  ProductType =>UintOfWork
            var Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            //2. Mapping To BrandResultDtos=>Imapper

            #region Manual Mapping
            //var typeResultDto = Types.Select(Types => new TypeResultDto
            //{
            //    Id = Types.Id,
            //    Name = Types.Name,
            //}).ToList();
            #endregion

            var typeResultDto = _mapper.Map<IEnumerable<TypeResultDto>>(Types);
            //3. Return typeResultDto
            return typeResultDto;
        }


    }
}
