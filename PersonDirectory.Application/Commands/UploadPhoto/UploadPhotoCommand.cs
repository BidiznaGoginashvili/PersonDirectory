using Microsoft.AspNetCore.Http;
using PersonDirectory.Shared.Infrastructure;

namespace PersonDirectory.Application.Commands.UploadPhoto
{
    public class UploadPhotoCommand : BaseCommand
    {
        public int Id { get; set; }
        public IFormFile Photo { get; set; }
    }
}
