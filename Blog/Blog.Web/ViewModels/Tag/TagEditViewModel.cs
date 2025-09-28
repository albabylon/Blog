using Blog.Application.Contracts.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Blog.Web.ViewModels.Tag
{
    public class TagEditViewModel : IValidatableObject
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            var tagService = validationContext.GetService<ITagService>();

            if (tagService.IsTagNameExistAsync(Name).GetAwaiter().GetResult())
                errors.Add(new ValidationResult($"Такое имя тега уже существует {Name}"));

            return errors;
        }

    }
}
