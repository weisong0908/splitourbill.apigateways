using System;
using AutoMapper;
using Web.Users.Models;
using Web.Users.Models.RequestModels;
using Web.Users.Models.ResponseModels;

namespace Web.Users.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BillAddRequest, Bill>()
                .ForMember(b => b.Id, opt => opt.MapFrom(bar => Guid.NewGuid()));

            CreateMap<BillUpdateRequest, Bill>();

            CreateMap<User, UserSimpleResponse>();

            CreateMap<UserSignUpRequest, User>()
                .ForMember(u => u.Id, opt => opt.MapFrom(usur => Guid.NewGuid()));
        }
    }
}