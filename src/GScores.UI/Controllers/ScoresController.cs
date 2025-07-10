using Microsoft.AspNetCore.Mvc;

namespace GScores.UI.Controllers;

public class ScoresController : BaseApiController
{
    [HttpGet]
    public IActionResult GetScores()
    {
        // Logic to retrieve scores
        return Ok(new[] { 100, 200, 300 });
    }
}