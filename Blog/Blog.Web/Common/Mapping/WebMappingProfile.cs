using AutoMapper;
using Blog.DTOs.User;
using Blog.Web.ViewModels;

namespace Blog.Web.Common.Mapping
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<CreateUserDTO, LoginViewModel>()
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.Nickname));
        }
    }
}
