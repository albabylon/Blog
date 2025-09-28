namespace Blog.Web.ViewModels.Article
{
    public class ArticleViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public IEnumerable<string> TagNames { get; set; }
        public bool IsPublished { get; set; } = true;
    }
}
