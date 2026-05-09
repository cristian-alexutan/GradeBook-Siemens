using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Repositories;

public class GradeRepository : IGradeReader
{
    private readonly List<Grade> _grades = new();
    private int _nextId = 1;

    public Task<Grade?> GetByIdAsync(int id)
    {
        var item = _grades.FirstOrDefault(i => i.Id == id && i.IsActive);
        return Task.FromResult(item);
    }

    public Task<IEnumerable<Grade>> GetAllAsync()
    {
        var items = _grades.Where(i => i.IsActive).AsEnumerable();
        return Task.FromResult(items);
    }
}
