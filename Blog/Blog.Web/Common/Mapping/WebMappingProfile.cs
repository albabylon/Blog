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
            CreateMap<ProfileEditViewModel, EditUserDTO>();

            CreateMap<ArticleDTO, ArticleViewModel>()
                .ForMember(d => d.TagNames, opt => opt.MapFrom(s => s.Tags.Select(t => t.Name)))
                .ForMember(d => d.PartOfContent, opt => opt.MapFrom(s => string.Join("", s.Content.Take(150))));
            CreateMap<ArticleViewModel, CreateArticleDTO>();
            CreateMap<EditArticleDTO, ArticleViewModel>();

            CreateMap<TagViewModel, CreateTagDTO>();
            CreateMap<TagEditViewModel, EditTagDTO>();

            CreateMap<CommentViewModel, CreateCommentDTO>();
            CreateMap<CommentEditViewModel, EditCommentDTO>();
        }
    }
}
