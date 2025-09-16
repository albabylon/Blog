using AutoMapper;
using Blog.Domain.Entities;
using Blog.DTOs.Article;

namespace Blog.Application.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Article, ArticleDTO>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.UserName));

            CreateMap<CreateArticleDTO, Article>();
        }
    }
}
