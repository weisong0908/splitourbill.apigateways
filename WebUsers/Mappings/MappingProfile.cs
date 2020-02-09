using AutoMapper;
using WebUsers.Models;
using WebUsers.Models.ResponseModels;

namespace WebUsers.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserSimpleResponse>();
        }
    }
}