using AutoMapper;
using Sat.Recruitment.Application.Responses;
using Sat.Recruitment.Application.Users.Commands;

namespace Sat.Recruitment.Application.Common.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<Sat.Recruitment.Core.Entities.User, UserResponse>().ReverseMap();
            CreateMap<Sat.Recruitment.Core.Entities.User, CreateUserCommand>().ReverseMap();
        }
    }
}
