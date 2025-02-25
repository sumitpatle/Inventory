using Application.Common.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Upload
{
    public class UploadCommand : IRequest<string>
    {
        public required IFormFile file { get; set; }
        public required string fileType { get; set; }
    }

    public class InventoryUploadCommandHandler : IRequestHandler<UploadCommand, string>
    {
        private readonly IUploadService _uploadService;
        private readonly ILogger<UploadCommand> _logger;

        public InventoryUploadCommandHandler(IUploadService uploadService,
                                      ILogger<UploadCommand> logger)
        {
            _uploadService = uploadService;
            _logger = logger;
        }

        public async Task<string> Handle(UploadCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string result = string.Empty;
                if (request.file == null || request.file.Length == 0)
                {
                    return "No file uploaded.";
                }
                if (request.fileType.ToUpper() == "MEMBER")
                {
                    result = await _uploadService.UploadMembers(request, cancellationToken);
                }
                else if(request.fileType.ToUpper() == "INVENTORY")
                {
                    result = await _uploadService.UploadInventory(request, cancellationToken);
                }
                return result;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }

        }
    }
}
