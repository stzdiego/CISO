using CISO.AuditService.Access.DbContext;
using CISO.AuditService.Shared.Entities;
using CISO.EvidenceService.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using STZ.Shared.Bases;

namespace CISO.AuditService.Backend.Controllers;

public class RegulationsController : StzControllerBase<Regulation>
{
    private readonly ILogger<RegulationsController> _logger;
    private readonly AuditServiceContext _context;
    private readonly ServiceBase<Attachment> _attachmentService;
    private readonly ServiceBase<RegulationSection> _sectionService;
    
    public RegulationsController(ILogger<RegulationsController> logger, AuditServiceContext context,
        ServiceBase<Attachment> attachmentService, ServiceBase<RegulationSection> sectionService) : base(logger, context)
    {
        _logger = logger;
        _context = context;
        _attachmentService = attachmentService;
        _sectionService = sectionService;
    }

    protected override async Task OnAfterPostAsync(Regulation entity)
    {
        var sectionPlain = new RegulationSection() { Number = "1", Title = "General.Plan", IdRegulation = entity.Id };
        var sectionTo = new RegulationSection() { Number = "2", Title = "General.Do", IdRegulation = entity.Id };
        var sectionDo = new RegulationSection() { Number = "3", Title = "General.Check", IdRegulation = entity.Id };
        var sectionCheck = new RegulationSection() { Number = "4", Title = "General.Act", IdRegulation = entity.Id };
        
        _context.Add(sectionPlain);
        _context.Add(sectionTo);
        _context.Add(sectionDo);
        _context.Add(sectionCheck);
        
        await _context.SaveChangesAsync();
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