using AutoMapper;
using UAE.Api.ViewModels.OrderViewModels;
using UAE.Application.Models.Order;

namespace UAE.Api.Mapper.Profiles
{
    public class ApiToBusinessModelMapperProfile : Profile
    {
        public ApiToBusinessModelMapperProfile()
        {
            CreateMap<OrderViewModel, OrderModel>()
                .ReverseMap();            
        }
    }
}