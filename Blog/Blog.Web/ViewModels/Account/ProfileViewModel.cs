using Blog.Web.ViewModels.Article;
using System.ComponentModel;

namespace Blog.Web.ViewModels.Account
{
    public class ProfileViewModel
    {
        public ProfileViewModel()
        {
            ProfilePictureUrl = "../images/avatar/avatar-deafult.png";
        }

        public string Id { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public IList<string> Role { get; set; }
        public string? Bio { get; set; }

        [DefaultValue("../images/avatar/avatar-deafult.png")]
        public string? ProfilePictureUrl { get; set; }
        
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public IEnumerable<ArticleViewModel> Articles { get; set; }
    }
}
