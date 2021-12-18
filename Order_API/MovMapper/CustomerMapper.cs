using AutoMapper;
using Order_API.Models;
using Order_API.Models.Dtos;

namespace Order_API.MovMapper
{
    public class CustomerMapper : Profile
    {
        public CustomerMapper()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, OrderUpdateDto>().ReverseMap();
            CreateMap<Order, OrderCreateDto>().ReverseMap();
        }
         
    }
}
