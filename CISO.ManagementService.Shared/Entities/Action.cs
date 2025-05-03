using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using STZ.Shared.Bases;

namespace CISO.ManagementService.Shared.Entities;

[Index(nameof(Name), IsUnique = true)]
public class Action : AuditBase<Guid>
{
    [Required] 
    [StringLength(50)] 
    public string Name { get; set; } = null!;
}