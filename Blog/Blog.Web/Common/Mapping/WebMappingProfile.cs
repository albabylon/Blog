using AutoMapper;
using Blog.DTOs.Article;
using Blog.DTOs.Comment;
using Blog.DTOs.Tag;
using Blog.DTOs.User;
using Blog.Web.ViewModels.Account;
using Blog.Web.ViewModels.Article;
using Blog.Web.ViewModels.Comment;
using Blog.Web.ViewModels.Tag;

namespace Blog.Web.Common.Mapping
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<UserDTO, ProfileViewModel>();
            CreateMap<LoginViewModel, LoginUserDTO>();
            CreateMap<RegisterViewModel, CreateUserDTO>();
            CreateMap<UserDTO, ProfileEditViewModel>();
            CreateMap<ProfileEditViewModel, EditUserDTO>();

            CreateMap<ArticleDTO, ArticleViewModel>()
                .ForMember(d => d.Tags, opt => opt.MapFrom(s => s.Tags))
                .ForMember(d => d.TagNames, opt => opt.MapFrom(s => s.Tags.Select(x => x.Name)))
                .ForMember(d => d.PartOfContent, opt => opt.MapFrom(s => string.Join("", s.Content.Take(150))));
            CreateMap<ArticleViewModel, CreateArticleDTO>();
            CreateMap<ArticleViewModel, EditArticleDTO>();
            CreateMap<EditArticleDTO, ArticleViewModel>();

            CreateMap<TagViewModel, TagDTO>();
            CreateMap<TagDTO, TagViewModel>();
            CreateMap<TagViewModel, CreateTagDTO>();
            CreateMap<TagEditViewModel, EditTagDTO>();

            CreateMap<CommentDTO, CommentViewModel>()
                .ForMember(d => d.User, opt => opt.MapFrom(s => s.AuthorName));
            CreateMap<CommentViewModel, CreateCommentDTO>();
            CreateMap<CommentEditViewModel, EditCommentDTO>();
        }
    }
}
