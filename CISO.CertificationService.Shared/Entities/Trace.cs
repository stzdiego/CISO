using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CISO.AuditService.Shared.Entities;
using STZ.Shared.Bases;
using STZ.Shared.Entities;

namespace CISO.CertificationService.Shared.Entities;

public class Trace : AuditBase<Guid>
{
    [Required]
    public Guid CompanyId { get; set; }
    
    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    public Guid RequirementId { get; set; }
    
    [Required]
    [StringLength(250)]
    public string Description { get; set; }
}