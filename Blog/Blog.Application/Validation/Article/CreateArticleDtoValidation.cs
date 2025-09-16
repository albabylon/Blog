using Blog.DTOs.Article;
using FluentValidation;

namespace Blog.Application.Validation.Article
{
    public class CreateArticleDtoValidation : AbstractValidator<CreateArticleDTO>
    {
        public CreateArticleDtoValidation()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Заголовок обязателен")
                .MaximumLength(200)
                .WithMessage("Заголовок не должен превышать 200 символов");
            
            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("Содержание обязательно");
        }

        //private bool BeSupported(string location)
        //{
        //    // Проверим, содержится ли значение в списке допустимых
        //    return Values.ValidRooms.Any(e => e == location);
        //}
    }
}
