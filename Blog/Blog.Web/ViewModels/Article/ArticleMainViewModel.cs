using Blog.Web.ViewModels.Comment;

namespace Blog.Web.ViewModels.Article
{
    public class ArticleMainViewModel
    {
        public ArticleViewModel Article { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}
