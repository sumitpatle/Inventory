using Application.Upload;

namespace Application.Common.Interface
{
    public interface IUploadService
    {
        Task<string> UploadMembers(UploadCommand file, CancellationToken cancellationToken);
        Task<string> UploadInventory(UploadCommand file, CancellationToken cancellationToken);
    }
}
