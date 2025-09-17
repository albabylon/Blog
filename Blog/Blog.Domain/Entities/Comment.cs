using Blog.Domain.Base;
using Blog.Domain.Identity;

namespace Blog.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }

        public int ArticleId { get; set; }
        public Article? Article { get; set; }

        public string UserId { get; set; }
        public User? User { get; set; }
    }
}
