using Blog.Application.Contracts.Interfaces;
using Blog.DTOs.Tag;
using FluentValidation;

namespace Blog.Application.Validation.Tag
{
    public class CreateTagDtoValidation : AbstractValidator<CreateTagDTO>
    {
        private readonly ITagService _tagService;

        public CreateTagDtoValidation(ITagService tagService)
        {
            _tagService = tagService;

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Имя тега обязателено")
                .MaximumLength(100)
                .WithMessage("Имя тега не должно превышать 100 символов")
                .MustAsync(BeUniq)
                .WithMessage("Имя тега уже существует");
        }

        private async Task<bool> BeUniq(string name, CancellationToken ct)
        {
            var tags = await _tagService.GetAllTagsAsync();

            if (tags == null || !tags.Any())
                return true;

            return tags.Select(t => t.Name).All(n => n != name);
        }
    }
}
