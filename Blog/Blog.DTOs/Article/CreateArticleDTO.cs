namespace Blog.DTOs.Article
{
    public class CreateArticleDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public IEnumerable<string> TagNames { get; set; }
        public bool IsPublished { get; set; } = true;
    }
}
