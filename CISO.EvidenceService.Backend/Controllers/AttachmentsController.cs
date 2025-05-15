using CISO.EvidenceService.Access.DbContext;
using CISO.EvidenceService.Backend.Services;
using CISO.EvidenceService.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using STZ.Shared.Bases;

namespace CISO.EvidenceService.Backend.Controllers;

public class AttachmentsController : StzControllerBase<Attachment>
{
    private readonly ILogger<AttachmentsController> _logger;
    private readonly EvidenceServiceContext _context;
    private readonly IStorageService _storageService;

    public AttachmentsController(ILogger<AttachmentsController> logger, EvidenceServiceContext context, IStorageService storageService) : base(logger, context)
    {
        _logger = logger;
        _context = context;
        _storageService = storageService;
    }
    
    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No se ha proporcionado un archivo válido");
        }

        try
        {
            await using var stream = file.OpenReadStream();
            var attachment = await _storageService.UploadFileAsync(
                stream, 
                file.FileName, 
                file.ContentType);

            await _context.Attachments.AddAsync(attachment);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Archivo subido con éxito: {FileName}", file.FileName);
            
            return CreatedAtAction(nameof(Get), new { id = attachment.Id }, attachment);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al subir el archivo {FileName}", file.FileName);
            return StatusCode(500, "Error al procesar el archivo");
        }
    }
    
    [HttpDelete]
    [Route("delete/{id}")]
    public async Task<IActionResult> DeleteFile(Guid id)
    {
        try
        {
            // Buscamos primero el adjunto en la base de datos
            var attachment = await _context.Attachments.FindAsync(id);
        
            if (attachment == null)
            {
                return NotFound($"No se encontró un archivo con el ID {id}");
            }
        
            // Eliminamos el archivo en MinIO
            await _storageService.DeleteFileAsync(attachment);
    
            // Eliminamos la entidad de la base de datos
            _context.Remove(attachment);
            await _context.SaveChangesAsync();
    
            _logger.LogInformation("Archivo eliminado con éxito: ID {FileId}", id);
    
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar el archivo con ID {FileId}", id);
            return StatusCode(500, "Error al eliminar el archivo");
        }
    }

    [HttpGet("download/{id}")]
    public async Task<IActionResult> DownloadFile(Guid id)
    {
        try
        {
            // Buscamos primero el adjunto en la base de datos
            var attachment = await _context.Attachments.FindAsync(id);
        
            if (attachment == null)
            {
                return NotFound($"No se encontró un archivo con el ID {id}");
            }
        
            var (fileStream, contentType, fileName) = await _storageService.DownloadFileAsync(attachment);
    
            _logger.LogInformation("Archivo descargado con éxito: {FileName}", fileName);
    
            return File(fileStream, contentType, fileName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al descargar el archivo con ID {FileId}", id);
            return StatusCode(500, "Error al descargar el archivo");
        }
    }
}