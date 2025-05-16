using CISO.AuditService.Access.DbContext;
using CISO.AuditService.Shared.Entities;
using CISO.EvidenceService.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using STZ.Shared.Bases;

namespace CISO.AuditService.Backend.Controllers;

public class RequerimentsController : StzControllerBase<Requeriment>
{
    private readonly ILogger<RequerimentsController> _logger;
    private readonly AuditServiceContext _context;
    private readonly ServiceBase<Attachment> _attachmentService;

    public RequerimentsController(ILogger<RequerimentsController> logger, AuditServiceContext context,
        ServiceBase<Attachment> attachmentService) : base(logger, context)
    {
        _logger = logger;
        _context = context;
        _attachmentService = attachmentService;
    }
    
    [HttpPost]
    [Route("Attachment/{id}/{idAttachment}")]
    public async Task<IActionResult> UploadAttachment(Guid id, Guid idAttachment)
    {
        try
        {
            var regulationAttach = new RegulationAttachment
            {
                IdRegulation = id,
                IdAttachment = idAttachment
            };

            await _context.AddAsync(regulationAttach);
            await _context.SaveChangesAsync();

            return Ok(regulationAttach);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
    
    [HttpGet]
    [Route("GetAttachments/{idRegulation}")]
    public async Task<IActionResult> GetAttachments(Guid idRegulation)
    {
        try
        {
            var attachmentsIds = _context.Set<RegulationAttachment>()
                .Where(x => x.IdRegulation == idRegulation)
                .ToList();
            
            if (!attachmentsIds.Any()) 
                return Ok(new List<Attachment>());
            
            var strFilter = "Id IN (" + string.Join(",", attachmentsIds.Select(x => "Guid(\"" + x.IdAttachment.ToString() + "\")")) + ")";
            var attachments = await _attachmentService.FindAsync(strFilter);
            
            return Ok(attachments);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
}