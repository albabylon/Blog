using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Domain.Identity
{
    [Comment("Таблица ролей")]
    public class Role : IdentityRole
    {
        public Role()
        {
            
        }

        public Role(string name) : base(name)
        {
            
        }
    }
}
