using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CISO.AuditService.Shared.Entities;
using STZ.Shared.Bases;
using STZ.Shared.Entities;

namespace CISO.CertificationService.Shared.Entities;

public class RegulationCompany : AuditBase<Guid>
{
    [Required]
    public Guid RegulationId { get; set; }
    
    [Required]
    public Guid CompanyId { get; set; }
    
    [Required] 
    public Guid UserResponsibleId { get; set; }

    [Required]
    public DateTime AuditDate { get; set; } = DateTime.UtcNow.AddMonths(2);
}