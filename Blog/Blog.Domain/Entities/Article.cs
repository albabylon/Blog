using Blog.Domain.Base;
using Blog.Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Domain.Entities
{
    [Comment("Таблица статей")]
    public class Article : BaseEntity
    {
        public string? Title { get; set; }
        public string? Content { get; set; }

        public bool IsPublished { get; set; } = true;

        public string AuthorId { get; set; }
        public User? Author { get; set; }

        public virtual ICollection<Comment>? Comments { get; set; }
        public virtual ICollection<ArticleTag>? ArticleTags { get; set; }
    }
}
