using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Services
{
    public interface IGradeService
    {
        Task<IEnumerable<Grade>> GetAllGradesAsync();
        Task<IEnumerable<Grade>> GetFirstNPassingActiveGradesAsync(int n);
        Task<Grade?> GetGradeByIdAsync(int id);
        Task<GradeStatistics> GetStatisticsAsync();
    }
}