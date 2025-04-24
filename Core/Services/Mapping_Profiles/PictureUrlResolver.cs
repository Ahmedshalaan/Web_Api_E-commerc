using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Shared.Dto;

namespace Services.Mapping_Profiles
{
    //Install Microsoft.Extensions.Configuration for Services ==> I need to use IConfiguration
    public class PictureUrlResolver(IConfiguration _configuration)  : IValueResolver<Product, ProductResultDto, string>
    {
        
        public string Resolve(Product source, ProductResultDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrWhiteSpace(source.PictureUrl))
            {
                /// appsetting.json => "BaseUrl" :"https://localhost:7259/",

                return _configuration["BaseUrl"] + source.PictureUrl;
                //return $"{_configuration["BaseUrl"]}{source.PictureUrl}";
            }
            return string.Empty;
        } 
    }
     
}
