
using CISO.AuditService.Shared.Entities;
using CISO.CertificationService.Access.DbContext;
using CISO.CertificationService.Shared.Dtos;
using CISO.CertificationService.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STZ.Shared.Bases;
using STZ.Shared.Entities;

namespace CISO.CertificationService.Backend.Controllers;

public class TracesController : StzControllerBase<Trace>
{
    private readonly ILogger<TracesController> _logger;
    private readonly CertificationServiceContext _context;
    private readonly ServiceBase<Regulation> _regulationService;
    private readonly ServiceBase<RegulationSection> _sectionsService;
    private readonly ServiceBase<Requirement> _requirementsService;
    private readonly ServiceBase<RegulationCompany> _regulationsCompanyService;
    private readonly ServiceBase<Trace> _tracesService;

    public TracesController(
        ILogger<TracesController> logger, 
        CertificationServiceContext context,
        ServiceBase<Regulation> regulationService,
        ServiceBase<RegulationSection> sectionsService,
        ServiceBase<Requirement> requirementsService,
        ServiceBase<RegulationCompany> regulationsCompanyService,
        ServiceBase<Trace> tracesService) : base(logger, context)
    {
        _logger = logger;
        _context = context;
        _regulationService = regulationService;
        _sectionsService = sectionsService;
        _requirementsService = requirementsService;
        _regulationsCompanyService = regulationsCompanyService;
        _tracesService = tracesService;
    }
    
    [HttpPost]
    [Route("update-traces")]
    public async Task<IActionResult> UpdateTracesByRegulation([FromBody] UpdateTracesRequest request)
    {
        try
        {
            var company = request.Company;
            var user = request.User;
            var items = request.Items.Where(x => x != null).ToList();

            // Trazas existentes para la compañía
            var existingTraces = await _context.Traces
                .Where(t => t.CompanyId == company.Id && !t.IsDeleted)
                .ToListAsync();

            var newRequirementIds = items.Select(r => r.Id).ToHashSet();
            var existingRequirementIds = existingTraces.Select(t => t.RequirementId).ToHashSet();

            // Crear nuevas trazas para requerimientos que no existan
            var toAdd = items.Where(r => !existingRequirementIds.Contains(r.Id)).ToList();
            foreach (var req in toAdd)
            {
                var trace = new Trace
                {
                    Id = Guid.NewGuid(),
                    CompanyId = company.Id,
                    UserId = user.Id,
                    RequirementId = req.Id,
                    Description = req.Title
                };
                _context.Traces.Add(trace);
            }

            // Eliminar trazas que ya no están en la lista nueva
            var toRemove = existingTraces.Where(t => !newRequirementIds.Contains(t.RequirementId)).ToList();
            _context.Traces.RemoveRange(toRemove);

            await _context.SaveChangesAsync();
            return Ok("Traces updated successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating traces");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    [Route("get-traces")]
    public async Task<IActionResult> GetTracesByCompany([FromBody] Guid companyId)
    {
        try
        {
            var traces = await _context.Traces.Where(x => x.CompanyId == companyId && !x.IsDeleted)
                .ToListAsync();

            return Ok(traces);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting traces");
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpPost]
    [Route("get-regulations-with-all-traces")]
    public async Task<IActionResult> GetRegulationsWithAllTraces([FromBody] Guid companyId)
    {
        try
        {
            // Obtener todas las regulaciones usando el servicio
            var regulationsCompany = await _regulationsCompanyService.FindAsync($"CompanyId = Guid(\"{companyId.ToString()}\")");
            var regulations = new List<Regulation>();

            foreach (var regulationCompany in regulationsCompany)
            {
                var totalRequirements = 0;
                var finishedRequirements = 0;
                var requirements = new List<Requirement>();
                
                var regulation = await _regulationService.GetByIdAsync(regulationCompany.RegulationId.ToString());
                
                // Obtener secciones de la regulación
                var sections = await _sectionsService.FindAsync($"IdRegulation = Guid(\"{regulationCompany.RegulationId.ToString()}\")");
                
                // Obtengo los requerimientos de cada sección
                foreach (var section in sections)
                {
                    var resRequirements = await _requirementsService.FindAsync($"IdRegulationSection = Guid(\"{section.Id.ToString()}\")");
                    requirements.AddRange(resRequirements);
                }
                totalRequirements += requirements.Count;
                
                // Obtener trazas de la compañia
                var traces = await _tracesService.FindAsync($"CompanyId = Guid(\"{companyId.ToString()}\")");
                
                // Si el Id del requerimiento de la traza está en la lista de requerimientos de la regulación sumamos 1
                foreach (var trace in traces)
                {
                    if (requirements.Any(x => x.Id.Equals(trace.RequirementId)))
                    {
                        finishedRequirements += 1;
                    }
                }

                if (totalRequirements == finishedRequirements)
                {
                    regulations.Add(regulation);
                }
            }

            return Ok(regulations);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error obteniendo regulaciones con todas sus trazas");
            return StatusCode(500, "Internal server error");
        }
    }
    
}