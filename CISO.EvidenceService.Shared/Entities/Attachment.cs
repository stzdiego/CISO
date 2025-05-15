using System.ComponentModel.DataAnnotations;
using CISO.EvidenceService.Shared.Enums;
using STZ.Shared.Bases;

namespace CISO.EvidenceService.Shared.Entities;

public class Attachment : AuditBase<Guid>
{
    [Required]
    [StringLength(250)]
    public string Name { get; set; } = null!;
    
    [Required]
    [StringLength(2000)]
    public string Url { get; set; } = null!;
    
    [Required]
    public FileTypeEnum Type { get; set; }
    
    [Required]
    public long Size { get; set; }
}