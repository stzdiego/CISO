using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using STZ.Shared.Bases;

namespace CISO.AuditService.Shared.Entities;

public class Requirement : AuditBase<Guid>
{
    [Required]
    [StringLength(10)]
    public string Number { get; set; } = null!;
    
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = null!;
    
    public string? Description { get; set; }
    
    [Required]
    public bool IsMandatory { get; set; } = true;
    
    [Required]
    public Guid IdRegulationSection { get; set; }
    
    [ForeignKey(nameof(IdRegulationSection))]
    public RegulationSection? RegulationSection { get; set; }
}