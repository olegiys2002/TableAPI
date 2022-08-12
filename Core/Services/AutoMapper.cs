using AutoMapper;
using Core.DTOs;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<OrderFormDTO, Order>().ReverseMap();
            CreateMap<Order, OrderDTO>().ForMember(order => order.TableDTO , opt => opt.MapFrom(orderDTO=>orderDTO.Table)).ReverseMap();
            CreateMap<TableFormDTO, Table>().ReverseMap();
            CreateMap<Table, TableDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserFormDTO, User>().ReverseMap();
        

        }
    }
}
