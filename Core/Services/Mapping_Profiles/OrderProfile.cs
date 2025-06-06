﻿using Domain.Entities.Idenetity;
using Domain.Entities.orderEntities;
using Shared.orderDto;
using OrderAddress = Domain.Entities.orderEntities.OrderAddress;

namespace Services.Mapping_Profiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile() {
            //Shiping Address
            CreateMap<OrderAddress,AddressDto>().ReverseMap();
            CreateMap<Address,AddressDto>().ReverseMap();

        CreateMap< DeliveryMethod,DeliveryMethodResultDto>();

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductName, options => options.MapFrom(s => s.prouductinOrderItem.ProductName))
                .ForMember(d => d.ProductId, options => options.MapFrom(s => s.prouductinOrderItem.ProductId))
                .ForMember(d => d.PictureUrl , options => options.MapFrom(s => s.prouductinOrderItem.PictureUrl));

            CreateMap<Order, OrderResultDto>()
                .ForMember(d => d.paymentStatus, options => options.MapFrom(s => s.paymentStatus.ToString()))
                .ForMember(d => d.deliveryMethod, options => options.MapFrom(s => s. deliveryMethod.ShortName))
                .ForMember(d => d.Total, options => options.MapFrom(s => s.SubTotal + s.deliveryMethod.Price))



                ;
        }
    }
}
