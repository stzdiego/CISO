using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using STZ.Shared.Bases;

namespace CISO.ManagementService.Shared.Entities;

[Index(nameof(PermissionId), nameof(RoleId), IsUnique = true)]
public class PermissionRole : AuditBase<Guid>
{
    [Required]
    public Guid PermissionId { get; set; }
    
    [Required]
    public Guid RoleId { get; set; }
    
    [ForeignKey(nameof(PermissionId))]
    public Permission? Permission { get; set; }
    
    [ForeignKey(nameof(RoleId))]
    public Role? Role { get; set; }
}