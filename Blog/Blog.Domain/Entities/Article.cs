using Blog.Domain.Base;
using Blog.Domain.Identity;

namespace Blog.Domain.Entities
{
    public class Article : BaseEntity
    {
        public string? Title { get; set; }
        public string? Content { get; set; }

        public bool IsPublished { get; set; } = false;

        public string AuthorId { get; set; }
        public User? Author { get; set; }

        public virtual ICollection<Comment>? Comments { get; set; }
        public virtual ICollection<ArticleTag>? ArticleTags { get; set; }
    }
}
