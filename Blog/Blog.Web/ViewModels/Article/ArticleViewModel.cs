namespace Blog.Web.ViewModels.Article
{
    public class ArticleViewModel
    {
        public string Title { get; set; }
        public string PartOfContent { get; set; }
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public IEnumerable<string> TagNames { get; set; }
        public bool IsPublished { get; set; } = true;
    }
}
