using Microsoft.AspNetCore.Mvc;
using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;
using Siemens.Internship2026.GradeBook.Services;

namespace Siemens.Internship2026.GradeBook.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GradeController : ControllerBase
{
    private readonly IGradeService _service;

    public GradeController(IGradeService service)
    {
        _service = service;
    }

    // Separate endpoints for getting all the grades and getting the statistics
    // This fits better with REST principles, as each endpoint has a single responsibility

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var grades = await _service.GetAllGradesAsync();

        return Ok(grades);
    }

    [HttpGet("statistics")]
    public async Task<IActionResult> GetStatistics()
    {
        var statistics = await _service.GetStatisticsAsync();

        return Ok(statistics);
    }

    [HttpGet("passing/top/{n}")]
    public async Task<IActionResult> GetFirstNPassingActive(int n)
    {
        if (n <= 0)
            return BadRequest("N must be a positive integer.");

        var grades = await _service.GetFirstNPassingActiveGradesAsync(n);
        return Ok(grades);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (id <= 0)
            return BadRequest("Id must be a positive integer.");

        var grade = await _service.GetGradeByIdAsync(id);
        if (grade == null)
            return NotFound($"Grade with Id {id} was not found.");

        return Ok(grade);
    }
}
