using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using Shared.Dto;
using Shared.ErrorModels;
using System.Net;

namespace Presentation
{
//[Authorize]                               //Primary Ctor               //install microsoft.AspNetCore.App               
    public class ProductsController(IService_Manager service_Manager) : ApiController
    {
        #region Get All Products
        [HttpGet] //Get L:BaseUrl/api/Products
        public async Task<ActionResult<PaginatedResult<ProductResultDto>>> GetAllProducts([FromQuery]ProductSpecParams parameters)
        {
            // Logic to get all products
            var Products = await service_Manager.ProductService.GetAllProductsAsync(parameters);
            return Ok(Products);
        }
        #endregion

        #region Get All Brands
        //Static Sgement =>>("Brands")
        [HttpGet("Brands")] //Get BaseUrl/api/Products/Brands
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrands()
        {
            // Logic to get all products
            var Brands = await service_Manager.ProductService.GetAllBrandsAsync();
            return Ok(Brands);
        }
        #endregion

        #region Get All Types
        //Static Sgement =>>("Types")
        [HttpGet("Types")] //Get BaseUrl/api/Products/Types
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypes()
        {
            // Logic to get all products
            var Types = await service_Manager.ProductService.GetAllTypesAsync();
            return Ok(Types);
        }
        #endregion

        #region Get All Product By Id
       
        [ProducesResponseType(typeof(ProductResultDto), (int)HttpStatusCode.OK)]
        [HttpGet("{Id}")] //Get BaseUrl/api/Products/5
        public async Task<ActionResult<ProductResultDto>> GetProductById(int Id)
        {
            // Logic to get all products
            var Product = await service_Manager.ProductService.GetProductByIdAsync(Id);
            if (Product is null)
                return NotFound();

            return Ok(Product);
        }
        #endregion

    }
}
