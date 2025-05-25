using AutoMapper;
using Entities;
using System.Runtime;
using DTO;

namespace SamsungStars
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            //CreateMap<Product, ProductDTO>()
            //    .ForMember(dest => dest.CategoryName,
            //               opts => opts.MapFrom(src => src.Category.CategoryId))
            //    .ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
        }
    }
}

