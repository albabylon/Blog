using Blog.Entities.Base;

namespace Blog.Contracts.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }

        public int ArticleId { get; set; }
        public Article Article { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
