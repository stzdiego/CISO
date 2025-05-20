using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using STZ.Shared.Bases;

namespace CISO.AuditService.Shared.Entities;

public class RequirementAttachment : AuditBase<Guid>
{
    [Required]
    public Guid IdAttachment { get; set; }
    
    [Required]
    public Guid IdRequeriment { get; set; }
    
    [ForeignKey(nameof(IdRequeriment))]
    public Requirement? Requeriment { get; set; }
}