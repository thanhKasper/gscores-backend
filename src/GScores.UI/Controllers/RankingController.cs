using GScores.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace GScores.UI.Controllers;

public class RankingController(IGroupRankingService groupRankingService) : BaseApiController
{
    private readonly IGroupRankingService _groupRankingService = groupRankingService;

    [HttpGet("group-A")]
    public async Task<IActionResult> GetTop10StudentsInAGroup()
    {
        var result = await _groupRankingService.GetTop10StudentsInAGroupAsync();
        return Ok(result);
    }
}