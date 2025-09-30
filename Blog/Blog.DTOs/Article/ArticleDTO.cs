using Blog.DTOs.Tag;

namespace Blog.DTOs.Article
{
    public class ArticleDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AuthorName { get; set; }
        public IEnumerable<TagDTO> Tags { get; set; }
    }
}
