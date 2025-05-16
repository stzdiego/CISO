using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using STZ.Shared.Bases;

namespace CISO.AuditService.Shared.Entities;

public class RegulationSection : AuditBase<Guid>
{
    [Required]
    [StringLength(10)]
    public string Number { get; set; } // Numeración (ej: "4.1")
    
    [Required]
    [StringLength(100)]
    public string Title { get; set; }
    
    [Required]
    public string ContentMD { get; set; }
    
    [Required] 
    public string ContentHTML { get; set; }
    
    [Required]
    public Guid IdRegulation { get; set; }
    
    [ForeignKey(nameof(IdRegulation))]
    public Regulation? Regulation { get; set; }
}