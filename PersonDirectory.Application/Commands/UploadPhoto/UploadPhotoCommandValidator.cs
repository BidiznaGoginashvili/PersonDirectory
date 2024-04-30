using FluentValidation;
using PersonDirectory.Application.Resources;

namespace PersonDirectory.Application.Commands.UploadPhoto
{
    public class UploadPhotoCommandValidator : AbstractValidator<UploadPhotoCommand>
    {
        public UploadPhotoCommandValidator()
        {
            RuleFor(command => command.Photo)
                .NotEmpty()
                .WithMessage("PhotoRequired".GetLocalizedResource());
        }
    }
}
