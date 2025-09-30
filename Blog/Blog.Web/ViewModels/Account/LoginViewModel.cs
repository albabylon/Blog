using System.ComponentModel.DataAnnotations;

namespace Blog.Web.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Необходимо ввести Email")]
        [EmailAddress]
        [Display(Name = "Email", Prompt = "Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Необходимо ввести Пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль", Prompt = "Пароль")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
