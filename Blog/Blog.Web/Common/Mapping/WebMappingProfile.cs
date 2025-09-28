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
            CreateMap<LoginViewModel, LoginUserDTO>();
            CreateMap<ProfileEditViewModel, EditUserDTO>();

            CreateMap<ArticleViewModel, CreateArticleDTO>();
            CreateMap<ArticleEditViewModel, EditArticleDTO>();

            CreateMap<TagViewModel, CreateTagDTO>();
            CreateMap<TagEditViewModel, EditTagDTO>();

            CreateMap<CommentViewModel, CreateCommentDTO>();
            CreateMap<CommentEditViewModel, EditCommentDTO>();
        }
    }
}
