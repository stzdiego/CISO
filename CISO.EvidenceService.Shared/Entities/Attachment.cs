using System.ComponentModel.DataAnnotations;
using STZ.Shared.Bases;

namespace CISO.EvidenceService.Shared.Entities;

public class Attachment : AuditBase<Guid>
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Url { get; set; }
    
    [Required]
    public string Type { get; set; }
    
    [Required]
    public int SizeKb { get; set; }
}