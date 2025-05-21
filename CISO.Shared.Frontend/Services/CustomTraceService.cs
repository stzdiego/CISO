using System.Net.Http.Json;
using CISO.AuditService.Shared.Entities;
using CISO.CertificationService.Shared.Dtos;
using CISO.CertificationService.Shared.Entities;
using Microsoft.Extensions.Configuration;
using STZ.Shared.Bases;
using STZ.Shared.Entities;

namespace CISO.Shared.Frontend.Services;

public class CustomTraceService : ServiceBase<Trace>
{
    public CustomTraceService(HttpClient httpClient, IConfiguration configuration) 
        : base(httpClient, configuration) { }

    public async Task UpdateTraceByRegulationAsync(Company company, User user, IEnumerable<Requirement> requirements)
    {
        try
        {
            var request = new UpdateTracesRequest()
            {
                Company = company,
                User = user,
                Items = requirements
            };

            var response = await HttpClient.PostAsJsonAsync($"{Endpoint}/update-traces", request);
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error updating traces: {errorMessage}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("An error occurred while updating traces.", e);
        }
    }
    
    public async Task<IEnumerable<Trace>> GetTracesByCompanyAsync(Company company)
    {
        try
        {
            var response = await HttpClient.PostAsJsonAsync($"{Endpoint}/get-traces", company.Id);
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al obtener trazas: {errorMessage}");
            }

            var traces = await response.Content.ReadFromJsonAsync<IEnumerable<Trace>>();
            return traces ?? Enumerable.Empty<Trace>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Ocurrió un error al obtener las trazas.", e);
        }
    }
    
    public async Task<int> GetRegulationsWithAllTracesAsync(Company company)
    {
        try
        {
            var response = await HttpClient.PostAsJsonAsync($"{Endpoint}/get-regulations-with-all-traces", company.Id);
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al obtener regulaciones: {errorMessage}");
            }

            var regulations = await response.Content.ReadFromJsonAsync<IEnumerable<Regulation>>();
            return regulations?.Count() ?? 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Ocurrió un error al obtener las regulaciones con todas sus trazas.", e);
        }
    }
}