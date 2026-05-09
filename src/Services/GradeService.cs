using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Services
{
    public class GradeService : IGradeService
    {
        private readonly IGradeReader _reader;
        private readonly ILogger<GradeService> _logger;

        public GradeService(IGradeReader reader, ILogger<GradeService> logger)
        {
            _reader = reader;
            _logger = logger;
        }

        public async Task<IEnumerable<Grade>> GetAllGradesAsync()
        {
            _logger.LogInformation("GET api/grade called");
            return await _reader.GetAllAsync();
        }

        public async Task<Grade?> GetGradeByIdAsync(int id)
        {
            _logger.LogInformation("GET api/grade/{Id} called", id);
            var grade = await _reader.GetByIdAsync(id);
            if (grade == null)
                _logger.LogWarning("Grade {Id} not found", id);
            return grade;
        }

        public async Task<IEnumerable<Grade>> GetFirstNPassingActiveGradesAsync(int n)
        {
            // The exercise specifies that this function should return "the first N grades"
            // This can either mean first N grades in the order of their insertion, or the top N grades by value
            // Here, I assumed it means the top N grades by value
            _logger.LogInformation("GET api/grade/passing/top called with N={N}", n);
            var grades = await _reader.GetAllAsync();
            var result = grades.Where(currentGrade => currentGrade.Value >= 5)
                .OrderByDescending(currentGrade => currentGrade.Value)
                .Take(n)
                .ToList();
            _logger.LogInformation("Returning {Count} passing active grades", result.Count);
            return result;
        }

        public async Task<GradeStatistics> GetStatisticsAsync()
        {
            var grades = await _reader.GetAllAsync();
            var list = grades.ToList();
            var stats = new GradeStatistics
            {
                TotalCount = list.Count,
                AverageValue = list.Any() ? list.Average(currentGrade => currentGrade.Value) : 0
            };
            _logger.LogInformation("Returning {TotalCount} grades, average value: {AverageValue}", stats.TotalCount, stats.AverageValue);
            return stats;
        }
    }
}
