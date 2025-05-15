using System.Transactions;
using CISO.EvidenceService.Access.DbContext;
using CISO.EvidenceService.Shared.Entities;
using CISO.EvidenceService.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Minio;
using Minio.ApiEndpoints;
using Minio.DataModel.Args;

namespace CISO.EvidenceService.Backend.Services;

public class MinioStorageService : IStorageService
{
    private readonly IMinioClient _minioClient;
    private readonly string _bucketName;
    private readonly string _minioEndpoint;
    private readonly EvidenceServiceContext _context;

    public MinioStorageService(IConfiguration configuration, EvidenceServiceContext context)
    {
        _minioEndpoint = configuration["Minio:Endpoint"] ?? "localhost:9000";
        _context = context;
        
        var minioAccessKey = configuration["Minio:AccessKey"] ?? "admin";
        var minioSecretKey = configuration["Minio:SecretKey"] ?? "minioadmin";
        var useSsl = bool.Parse(configuration["Minio:UseSsl"] ?? "false");
        _bucketName = configuration["Minio:BucketName"] ?? "ciso-evidence";

        _minioClient = new MinioClient()
            .WithEndpoint(_minioEndpoint)
            .WithCredentials(minioAccessKey, minioSecretKey)
            .WithSSL(useSsl)
            .Build();

        EnsureBucketExists().Wait();
    }

    private async Task EnsureBucketExists()
    {
        var bucketExistsArgs = new BucketExistsArgs().WithBucket(_bucketName);
        if (!await _minioClient.BucketExistsAsync(bucketExistsArgs))
        {
            await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(_bucketName));
        }
    }

    public async Task<Attachment> UploadFileAsync(Stream fileStream, string fileName, string contentType)
    {
        try
        {
            var attachment = new Attachment()
            {
                Id = Guid.NewGuid(),
                Name = fileName,
                Type = DetermineFileType(fileName)
            };

            var objectName = $"{attachment.Id}-{fileName}";
            var fileSize = fileStream.Length;

            var putObjectArgs = new PutObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(objectName)
                .WithStreamData(fileStream)
                .WithObjectSize(fileSize)
                .WithContentType(contentType);

            await _minioClient.PutObjectAsync(putObjectArgs);

            var endpoint = _minioEndpoint.EndsWith("/") ? _minioEndpoint : $"{_minioEndpoint}/";
            var url = $"{endpoint}{_bucketName}/{objectName}";

            attachment.Url = url;
            attachment.Size = fileSize;

            return attachment;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task DeleteFileAsync(Attachment attachment)
    {
        try
        {
            if (attachment == null)
                throw new ArgumentNullException(nameof(attachment));

            // El prefijo que buscamos es el ID del attachment
            var objectPrefix = attachment.Id.ToString();
        
            // Listar todos los objetos que comienzan con el ID
            var listArgs = new ListObjectsArgs()
                .WithBucket(_bucketName)
                .WithPrefix(objectPrefix);

            var objects = _minioClient.ListObjectsEnumAsync(listArgs).ConfigureAwait(false);

            // Eliminar cada objeto encontrado
            await foreach (var item in objects)
            {
                var removeObjectArgs = new RemoveObjectArgs()
                    .WithBucket(_bucketName)
                    .WithObject(item.Key);
                
                await _minioClient.RemoveObjectAsync(removeObjectArgs);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al eliminar el archivo: {ex.Message}", ex);
        }
    }

    public async Task<(Stream Stream, string ContentType, string FileName)> DownloadFileAsync(Attachment attachment)
    {
        try
        {
            if (attachment == null)
                throw new ArgumentNullException(nameof(attachment));

            // El nombre del objeto en MinIO sigue el formato: {attachmentId}-{filename}
            // Vamos a buscar objetos que empiecen con el ID del attachment
            var objectPrefix = attachment.Id.ToString();
            var objectName = string.Empty;

            // Listar todos los objetos que comienzan con el ID
            var listArgs = new ListObjectsArgs()
                .WithBucket(_bucketName)
                .WithPrefix(objectPrefix);

            var objects = _minioClient.ListObjectsEnumAsync(listArgs).ConfigureAwait(false);
        
            // Tomar el primer objeto que encontremos con ese prefijo
            await foreach (var item in objects)
            {
                objectName = item.Key;
                break; // Solo necesitamos el primer objeto
            }

            if (string.IsNullOrEmpty(objectName))
                throw new FileNotFoundException($"No se encontró el archivo en el almacenamiento");

            // Crear un MemoryStream para almacenar los datos
            var memoryStream = new MemoryStream();

            // Configurar los argumentos para obtener el objeto
            var getObjectArgs = new GetObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(objectName)
                .WithCallbackStream(stream =>
                {
                    stream.CopyTo(memoryStream);
                    memoryStream.Position = 0;
                });

            // Obtener el objeto de MinIO
            var stat = await _minioClient.GetObjectAsync(getObjectArgs);

            return (memoryStream, stat.ContentType, attachment.Name);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al descargar el archivo: {ex.Message}", ex);
        }
    }

    private FileTypeEnum DetermineFileType(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        return extension switch
        {
            ".pdf" => FileTypeEnum.Pdf,
            ".jpg" or ".jpeg" or ".png" => FileTypeEnum.Image,
            ".doc" or ".docx" => FileTypeEnum.Document,
            ".xls" or ".xlsx" => FileTypeEnum.Datasheet,
            ".zip" or ".rar" => FileTypeEnum.Archive,
            ".mp4" or ".avi" or ".mov" => FileTypeEnum.Video,
            ".mp3" or ".wav" => FileTypeEnum.Audio,
            ".txt" => FileTypeEnum.Text,
            ".exe" or ".sh" => FileTypeEnum.Executable,
            ".ppt" or ".pptx" => FileTypeEnum.Presentation,
            _ => FileTypeEnum.Other
        };
    }
}