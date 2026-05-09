using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Interfaces
{
    public interface IGradeApiClient
    {
        Task<IEnumerable<Grade>> FetchAllAsync();
    }
}