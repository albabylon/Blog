namespace Blog.DTOs.Article
{
    public class EditArticleDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public IEnumerable<string> TagNames { get; set; }
        public bool IsPublished { get; set; }
    }
}
