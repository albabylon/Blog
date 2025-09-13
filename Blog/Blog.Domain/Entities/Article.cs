using Blog.Entities.Base;

namespace Blog.Contracts.Entities
{
    public class Article : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public bool IsPublished { get; set; } = false;

        public int AuthorId { get; set; }
        public User Author { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ArticleTag> ArticleTags { get; set; }
    }
}
