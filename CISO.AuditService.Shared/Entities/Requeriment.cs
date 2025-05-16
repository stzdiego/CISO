using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using STZ.Shared.Bases;

namespace CISO.AuditService.Shared.Entities;

public class Requeriment : AuditBase<Guid>
{
    [Required]
    [StringLength(10)]
    public string Number { get; set; } = null!;
    
    [Required]
    [StringLength(20)]
    public string Title { get; set; } = null!;
    
    [Required]
    public string? Description { get; set; }
    
    [Required]
    public string ContentMD { get; set; }
    
    [Required] 
    public string ContentHTML { get; set; }
    
    [Required]
    public bool IsMandatory { get; set; } = true;
    
    [Required]
    public Guid IdRegulationSection { get; set; }
    
    [ForeignKey(nameof(IdRegulationSection))]
    public RegulationSection? RegulationSection { get; set; }
}