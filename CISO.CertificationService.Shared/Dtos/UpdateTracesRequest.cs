using CISO.AuditService.Shared.Entities;
using STZ.Shared.Entities;

namespace CISO.CertificationService.Shared.Dtos;

public class UpdateTracesRequest
{
    public Company Company { get; set; }
    public User User { get; set; }
    public IEnumerable<Requirement> Items { get; set; }
}