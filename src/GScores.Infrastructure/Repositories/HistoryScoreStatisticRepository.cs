using GScores.Core.Domains.RepositoryContracts;
using GScores.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GScores.Infrastructure.Repositories;

public class HistoryScoreStatisticRepository(ApplicationDbContext dbContext) :
    IScoreStatisticRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<List<int>> GetStatisticTotalStudentsAsync()
    {
        var result = await _dbContext.Students
            .AsNoTracking() // Improve performance by not tracking changes
            .Where(s => s.HistoryScore != null && s.IsNaturalScience == false)
            .GroupBy(s =>
                s.HistoryScore < 4 ? "Level 1" :
                s.HistoryScore < 6 ? "Level 2" :
                s.HistoryScore < 8 ? "Level 3" :
                "Level 4"
            )
            .Select(g => g.Count())
            .ToListAsync();
        return result;
    }
}