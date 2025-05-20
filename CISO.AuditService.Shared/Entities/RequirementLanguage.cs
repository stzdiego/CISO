using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using STZ.Shared.Bases;

namespace CISO.AuditService.Shared.Entities;

public class RequirementLanguage : AuditBase<Guid>
{
    [Required]
    public Guid RequirementId { get; set; }
    
    [Required]
    public Guid LanguageId { get; set; }

    public string Markdown { get; set; } = string.Empty;
    public string Html { get; set; } = string.Empty;
    
    [ForeignKey(nameof(RequirementId))]
    public virtual Requirement? Requirement { get; set; }
    
    [ForeignKey(nameof(LanguageId))]
    public virtual Language? Language { get; set; }
}