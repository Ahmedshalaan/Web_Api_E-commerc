using Shared.Dto;

namespace Services.Mapping_Profiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket, BasketDTo>()
                .ReverseMap();
            CreateMap<BasketItems, BasketItemDTo>().ReverseMap();
        }
    }
     
}
