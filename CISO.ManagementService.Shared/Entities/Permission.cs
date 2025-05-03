using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using STZ.Shared.Bases;

namespace CISO.ManagementService.Shared.Entities;

[Index(nameof(FeatureId), nameof(ActionId), nameof(RoleId), IsUnique = true)]
public class Permission : AuditBase<Guid>
{
    [Required]
    public Guid FeatureId { get; set; }
    
    [Required]
    public Guid ActionId { get; set; }
    
    [Required]
    public Guid RoleId { get; set; }
    
    [ForeignKey(nameof(FeatureId))]
    public Feature? Feature { get; set; }
    
    [ForeignKey(nameof(ActionId))]
    public Action? Action { get; set; }
    
    [ForeignKey(nameof(RoleId))]
    public Role? Role { get; set; }
}