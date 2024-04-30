using PersonDirectory.Shared.Infrastructure;
using PersonDirectory.Domain.PersonManagement.Repository;

namespace PersonDirectory.Application.Commands.UploadPhoto
{
    public class UploadPhotoCommandHandler : BaseCommandHandler<UploadPhotoCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPersonRepository _personRepository;

        public UploadPhotoCommandHandler(IUnitOfWork unitOfWork,
                                         IPersonRepository personRepository)
        {
            _unitOfWork = unitOfWork;
            _personRepository = personRepository;
        }

        public override async Task<BaseCommandResult> Handle(UploadPhotoCommand command, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(command.Id, cancellationToken);

            if (person == null)
                return await FailAsync(ErrorCode.NotFound);

            var photoPath = await UploadAsync(person.Id.ToString(), command, cancellationToken);

            person.SetPhotoPath(photoPath);

            await _personRepository.UpdateAsync(person, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return await OkAsync(person.Id);
        }

        private async Task<string> UploadAsync(string id, UploadPhotoCommand command, CancellationToken cancellationToken)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "photos");
            var uniqueFileName = id + "_" + command.Photo.FileName;
            var photoPath = Path.Combine(uploadsFolder, uniqueFileName);

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            using (var fileStream = new FileStream(photoPath, FileMode.Create))
            {
                await command.Photo.CopyToAsync(fileStream, cancellationToken);
            }

            return photoPath;
        }
    }
}
