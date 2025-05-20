using System.ComponentModel.DataAnnotations;
using STZ.Shared.Bases;

namespace CISO.AuditService.Shared.Entities;

public class Language : AuditBase<Guid>
{
    [Required]
    [StringLength(10)]
    public string Code { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
}