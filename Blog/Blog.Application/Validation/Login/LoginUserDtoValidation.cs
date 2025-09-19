using Blog.DTOs.User;
using FluentValidation;

namespace Blog.Application.Validation.Login
{
    public class LoginUserDtoValidation : AbstractValidator<LoginUserDTO>
    {
        public LoginUserDtoValidation()
        {
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Введите пароль");
        }
    }
}
