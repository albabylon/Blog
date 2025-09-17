using Blog.Domain.Base;
using Blog.Domain.Identity;

namespace Blog.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }

        public Guid ArticleId { get; set; }
        public Article? Article { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
