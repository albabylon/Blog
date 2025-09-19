using AutoMapper;
using Blog.Domain.Entities;
using Blog.Domain.Identity;
using Blog.DTOs.Article;
using Blog.DTOs.User;

namespace Blog.Application.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //user
            CreateMap<CreateUserDTO, User>()
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.Nickname));
            CreateMap<LoginUserDTO, User>();
            CreateMap<EditUserDTO, User>()
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.Nickname));
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();

            //article
            CreateMap<Article, ArticleDTO>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.UserName));
            CreateMap<CreateArticleDTO, Article>();
            CreateMap<EditArticleDTO, Article>();
        }
    }
}
