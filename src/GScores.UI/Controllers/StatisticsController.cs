using GScores.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace GScores.UI.Controllers;

public class StatisticsController(IScoreStatistic scoreStatistic) : BaseApiController
{
    private readonly IScoreStatistic _scoreStatistic = scoreStatistic;
    [HttpGet("{subject}")]
    public async Task<IActionResult> GetSubjectStatistic(string subject)
    {
        return Ok(await _scoreStatistic.GetStatisticTotalStudentsOfSubjectAsync(subject));
    }
}