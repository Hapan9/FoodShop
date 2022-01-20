using AutoMapper;
using BLL.Dto;
using DAL.Models;

namespace BLL
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>()
                .ReverseMap();
        }

        public static MapperConfiguration InitializeAutoMapper()
        {
            var mapperConfiguration = new MapperConfiguration(conf => conf.AddProfile(new AutoMapperProfile()));

            return mapperConfiguration;
        }
    }
}