using Microsoft.AspNetCore.Mvc;
using Siemens.Internship2026.GradeBook.Interfaces;

namespace Siemens.Internship2026.GradeBook.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GradeController : ControllerBase
{
    private readonly IGradeReader _reader;

    public GradeController(IGradeReader reader)
    {
        _reader = reader;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        Console.WriteLine($"[LOG] {DateTime.UtcNow}: GET api/grade called");

        var grades = await _reader.GetAllAsync();
        var gradeList = grades.ToList();

        var totalCount = gradeList.Count;
        var averageValue = gradeList.Any() ? gradeList.Average(i => i.Value) : 0;

        Console.WriteLine($"[LOG] Returning {totalCount} grades, average value: {averageValue}");

        return Ok(new
        {
            Data = gradeList,
            Statistics = new
            {
                TotalCount = totalCount,
                AverageValue = averageValue,
                RetrievedAt = DateTime.UtcNow
            }
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        Console.WriteLine($"[LOG] {DateTime.UtcNow}: GET api/grade/{id} called");

        if (id <= 0)
        {
            Console.WriteLine($"[LOG] Invalid id: {id}");
            return BadRequest("Id must be a positive integer.");
        }

        var grade = await _reader.GetByIdAsync(id);
        if (grade == null)
        {
            Console.WriteLine($"[LOG] Grade {id} not found");
            return NotFound($"Grade with Id {id} was not found.");
        }

        return Ok(grade);
    }
}
