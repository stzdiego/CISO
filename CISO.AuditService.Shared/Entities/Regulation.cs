using System.ComponentModel.DataAnnotations;
using CISO.AuditService.Shared.Enums;
using STZ.Shared.Bases;

namespace CISO.AuditService.Shared.Entities;

public class Regulation : AuditBase<Guid>
{
    [Required]
    [StringLength(100)]
    public string Code { get; set; } = null!;
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = null!;
    
    [StringLength(100)]
    public string? Description { get; set; }
    
    [StringLength(50)]
    public string? VersionRegulation { get; set; }
    
    [Required]
    public DateTime? PublicationDate { get; set; } = DateTime.UtcNow;
    
    [Required] 
    public RegulationStatusEnum Status { get; set; } = RegulationStatusEnum.Draft;
    
    [Required]
    [StringLength(100)]
    public string IssuingBody { get; set; } = null!;
    
    [Required]
    [StringLength(100)]
    public string Scope { get; set; } = null!;
    
    [Required]
    [StringLength(100)]
    public string Category { get; set; } = null!;
}