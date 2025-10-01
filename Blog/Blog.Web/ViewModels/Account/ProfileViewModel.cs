using Blog.Web.ViewModels.Article;

namespace Blog.Web.ViewModels.Account
{
    public class ProfileViewModel
    {
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public IList<string> Role { get; set; }
        public string? Bio { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public IEnumerable<ArticleViewModel> Articles { get; set; }
    }
}
