using Blog.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Blog.Domain.Entities
{
    [Comment("Таблица тегов")]
    public class Tag : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<ArticleTag>? ArticleTags { get; set; }
    }
}