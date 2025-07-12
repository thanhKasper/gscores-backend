using GScores.Core.DTOs;
using GScores.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace GScores.UI.Controllers;

public class ScoresController(IReadScoreService readScoreService) : BaseApiController
{
    private readonly IReadScoreService _readScoreService = readScoreService;


    [HttpGet("{studentId}")]
    public async Task<ActionResult<StudentScore>> GetScores(string? studentId)
    {
        try
        {
            // Logic to retrieve scores
            var studentScore = await _readScoreService.GetScoresAsync(studentId);
            return studentScore;
        }
        catch (ArgumentNullException)
        {
            return BadRequest("Student ID cannot be null or empty.");
        }
        catch (ArgumentException)
        {
            return NotFound("Cannot find student with the provided ID");
        }
        catch (Exception)
        {
            // Log the exception (not shown here for brevity)
            return StatusCode(500, "An unexpected error occurred.");
        }
    }
}