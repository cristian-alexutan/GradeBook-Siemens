using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Repositories;

public class GradeRepository : IGradeReader
{
    private readonly List<Grade> _grades = new();
    // private int _nextId = 1; <- unused field

    public Task<Grade?> GetByIdAsync(int id)
    {
        var grade = _grades.FirstOrDefault(currentGrade => currentGrade.Id == id && currentGrade.IsActive);
        return Task.FromResult(grade);
    }

    public Task<IEnumerable<Grade>> GetAllAsync()
    {
        var grades = _grades.Where(currentGrade => currentGrade.IsActive).AsEnumerable();
        return Task.FromResult(grades);
    }
}
