using Blog.Web.ViewModels.Article;
using Blog.Web.ViewModels.Tag;

namespace Blog.Web.ViewModels.Account
{
    public class AdminViewModel
    {
        public IEnumerable<ProfileViewModel> Users { get; set; }

        public IEnumerable<ArticleViewModel> Articles { get; set; }

        public IEnumerable<TagViewModel> Tags { get; set; }

        public AdminViewModel()
        {
            Users = new List<ProfileViewModel>();
            Articles = new List<ArticleViewModel>();
            Tags = new List<TagViewModel>();
        }
    }
}
