using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using STZ.Shared.Bases;

namespace CISO.AuditService.Shared.Entities;

public class RegulationAttachment : AuditBase<Guid>
{
    [Required]
    public Guid IdAttachment { get; set; }
    
    [Required]
    public Guid IdRegulation { get; set; }
    
    [ForeignKey(nameof(IdRegulation))]
    public Regulation? Regulation { get; set; }
}