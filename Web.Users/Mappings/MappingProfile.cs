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
                .ForMember(b => b.Id, opt => opt.MapFrom(bar => Guid.NewGuid()))
                .ForMember(b => b.TotalAmount, opt => opt.MapFrom(bar => Decimal.Parse(bar.TotalAmount)));

            CreateMap<BillUpdateRequest, Bill>()
                .ForMember(bur => bur.TotalAmount, opt => opt.MapFrom(bur => Decimal.Parse(bur.TotalAmount)));

            CreateMap<User, UserSimpleResponse>();

            CreateMap<UserSignUpRequest, User>()
                .ForMember(u => u.Id, opt => opt.MapFrom(usur => Guid.NewGuid()));
        }
    }
}