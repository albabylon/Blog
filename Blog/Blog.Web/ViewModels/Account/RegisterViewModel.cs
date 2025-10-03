using Blog.Domain.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blog.Web.ViewModels.Account
{
    public class RegisterViewModel
    {
        public RegisterViewModel()
        {
            ProfilePictureUrl = "../images/avatar/avatar-deafult.png";
        }

        [Required(ErrorMessage = "Поле Email обязательно для заполнения")]
        [EmailAddress]
        [Display(Name = "Email", Prompt = "example@example.com")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Поле Никнейм обязательно для заполнения")]
        [Display(Name = "Никнейм", Prompt = "Никнейм")]
        public string Nickname { get; set; }


        [Required(ErrorMessage = "Поле Пароль обязательно для заполнения")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль", Prompt = "Пароль")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Обязательно подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль", Prompt = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }


        [Display(Name = "О вас", Prompt = "О вас")]
        public string? Bio { get; set; }


        [DefaultValue("../images/avatar/avatar-deafult.png")]
        public string? ProfilePictureUrl { get; set; }
    }
}
