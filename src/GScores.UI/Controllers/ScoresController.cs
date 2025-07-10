using GScores.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace GScores.UI.Controllers;

public class ScoresController(IReadScoreService readScoreService) : BaseApiController
{
    private readonly IReadScoreService _readScoreService = readScoreService;

    
    [HttpGet("{studentId}")]
    public async Task<IActionResult> GetScores(string? studentId)
    {
        // Logic to retrieve scores
        return Ok(await _readScoreService.GetScoresAsync(studentId));
    }
}