using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models.Basket;
using Shared.Dto_s.BasketDto;

namespace Services.MappingProfiles
{
    public class BasketProfile:Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket, BasketDto>().ReverseMap();
            //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)) 
            //.ReverseMap()
            //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));


            CreateMap<BasketItem,BasketItemDto>().ReverseMap();
        }
    }
}
