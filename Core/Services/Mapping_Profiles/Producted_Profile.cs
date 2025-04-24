using Shared.Dto;

namespace Services.Mapping_Profiles
{
    public class Producted_Profile : Profile
    {
        public Producted_Profile()
        {
            //Create Mapping Profile For Product => ProductResultDto
            CreateMap<Product, ProductResultDto>()
                .ForMember(D=>D.BrandName,options=>options.MapFrom(S=>S.ProductBrand.Name)).
                 ForMember(D=>D.TypeName,options=>options.MapFrom(S=>S.ProductType.Name)).
                 ForMember(D=>D.PictureUrl,  options => options .MapFrom<PictureUrlResolver>());
            //Create Mapping Profile For Brand => BrandResultDto
            CreateMap<ProductBrand, BrandResultDto>();
            //Create Mapping Profile For Type => TypeResultDto
            CreateMap<ProductType, TypeResultDto>();

        }


    }
}
