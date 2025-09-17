using Microsoft.AspNetCore.Identity;

namespace Blog.Domain.Identity
{
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
