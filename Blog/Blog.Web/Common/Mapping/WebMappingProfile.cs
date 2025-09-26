using AutoMapper;
using Blog.DTOs.User;
using Blog.Web.ViewModels;

namespace Blog.Web.Common.Mapping
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<LoginViewModel, LoginUserDTO>();
        }
    }
}
