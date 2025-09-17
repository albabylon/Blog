using Blog.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Blog.Domain.Identity
{
    public class User : IdentityUser<Guid>
    {
        public string Nickname { get; set; }
        public string Bio { get; set; }
        public string ProfilePictureUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
