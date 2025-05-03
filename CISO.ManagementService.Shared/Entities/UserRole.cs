using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using STZ.Shared.Bases;

namespace CISO.ManagementService.Shared.Entities;

[Index(nameof(UserId), nameof(RoleId), IsUnique = true)]
public class UserRole : AuditBase<Guid>
{
    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    public Guid RoleId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
    
    [ForeignKey(nameof(RoleId))]
    public Role? Role { get; set; }
}