using Blog.Entities.Base;

namespace Blog.Contracts.Entities
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ArticleTag> ArticleTags { get; set; }
    }
}