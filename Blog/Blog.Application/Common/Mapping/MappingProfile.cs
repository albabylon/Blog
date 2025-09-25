using AutoMapper;
using Blog.Domain.Entities;
using Blog.Domain.Identity;
using Blog.DTOs.Article;
using Blog.DTOs.Comment;
using Blog.DTOs.Tag;
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

            //tags
            CreateMap<TagDTO, Tag>();
            CreateMap<Tag, TagDTO>();
            CreateMap<CreateTagDTO, Tag>();
            CreateMap<Tag, CreateTagDTO>();
            CreateMap<EditTagDTO, Tag>();
            CreateMap<Tag, EditTagDTO>();

            //comments
            CreateMap<CommentDTO, Comment>();
            CreateMap<Comment, CommentDTO>()
                .ForMember(d => d.AuthorName, s => s.MapFrom(x => x.User.UserName))
                .ForMember(d => d.ArticleName, s => s.MapFrom(x => x.Article.Title));
            CreateMap<CreateCommentDTO, Comment>();
            CreateMap<Comment, CreateCommentDTO>();
            CreateMap<EditCommentDTO, Comment>();
            CreateMap<Comment, EditCommentDTO>();
        }
    }
}
