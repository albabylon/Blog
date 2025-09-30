namespace Blog.Web.ViewModels.Account
{
    public class ProfileEditViewModel
    {
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string PasswordOld { get; set; }
        public string PasswordNew { get; set; }
        public string? Bio { get; set; }
        public string? ProfilePictureUrl { get; set; }
    }
}
