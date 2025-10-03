using System.ComponentModel;

namespace Blog.Web.ViewModels.Account
{
    public class ProfileEditViewModel
    {
        public ProfileEditViewModel()
        {
            ProfilePictureUrl = "../images/avatar/avatar-deafult.png";
        }

        public string Email { get; set; }
        public string Nickname { get; set; }
        public IList<string> Role { get; set; }
        public string? Bio { get; set; }

        [DefaultValue("../images/avatar/avatar-deafult.png")]
        public string? ProfilePictureUrl { get; set; }
        

        public DateTime? UpdatedAt { get; set; }
    }
}
