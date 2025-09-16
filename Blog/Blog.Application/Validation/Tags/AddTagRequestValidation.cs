using Blog.Application.Models.Tags;
using FluentValidation;

namespace Blog.Application.Validation.Tags
{
    public class AddTagRequestValidation : AbstractValidator<AddTagRequest>
    {
        public AddTagRequestValidation()
        {
            //RuleFor(x => x.Name).NotEmpty(); // Проверим на null и на пустое свойство
            //RuleFor(x => x.Manufacturer).NotEmpty();
            //RuleFor(x => x.Model).NotEmpty();
            //RuleFor(x => x.SerialNumber).NotEmpty();
            //RuleFor(x => x.CurrentVolts).NotEmpty().InclusiveBetween(120, 220); // Проверим, что значение в заданном диапазоне
            //RuleFor(x => x.GasUsage).NotNull();
            //RuleFor(x => x.RoomLocation).NotEmpty().Must(BeSupported).WithMessage($"Please choose one of the following locations: {string.Join(", ", Values.ValidRooms)}");
        }

        //private bool BeSupported(string location)
        //{
        //    // Проверим, содержится ли значение в списке допустимых
        //    return Values.ValidRooms.Any(e => e == location);
        //}
    }
}
