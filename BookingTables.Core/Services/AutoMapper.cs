using AutoMapper;
using Core.DTOs;
using Models.Models;

namespace Core.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<OrderFormDTO, Order>().ReverseMap();
            CreateMap<Order, OrderDTO>().ForMember(order => order.Tables , opt => opt.MapFrom(orderDTO=>orderDTO.Table)).ReverseMap();
            CreateMap<TableFormDTO, Table>().ReverseMap();
            CreateMap<Table, TableDTO>().ReverseMap();

        }
    }
}
