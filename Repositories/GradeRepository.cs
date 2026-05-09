using Siemens.Internship2026.GradeBook.ApiClients;
using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Repositories;

public class GradeRepository : IGradeReader
{
    private readonly IGradeApiClient _apiClient;

    public GradeRepository(IGradeApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<IEnumerable<Grade>> GetAllAsync()
    {
        return await _apiClient.FetchAllAsync();
    }

    public async Task<Grade?> GetByIdAsync(int id)
    {
        // The external API does not support querying by ID, so we fetch all grades and filter locally.
        var all = await _apiClient.FetchAllAsync();
        return all.FirstOrDefault(currentGrade => currentGrade.Id == id && currentGrade.IsActive);
    }
}