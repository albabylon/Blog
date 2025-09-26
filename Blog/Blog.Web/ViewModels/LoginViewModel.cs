namespace Blog.Web.ViewModels
{
    public class LoginViewModel
    {
        public string Email { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
