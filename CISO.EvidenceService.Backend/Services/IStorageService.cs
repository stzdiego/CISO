using CISO.EvidenceService.Shared.Entities;

namespace CISO.EvidenceService.Backend.Services;

public interface IStorageService
{
    Task<Attachment> UploadFileAsync(Stream fileStream, string fileName, string contentType);
    Task DeleteFileAsync(Attachment attachment);
    Task<(Stream Stream, string ContentType, string FileName)> DownloadFileAsync(Attachment attachment);
}