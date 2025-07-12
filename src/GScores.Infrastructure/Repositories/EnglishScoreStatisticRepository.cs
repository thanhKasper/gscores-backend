using GScores.Core.Domains.RepositoryContracts;
using GScores.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GScores.Infrastructure.Repositories;

public class EnglishScoreStatisticRepository(ApplicationDbContext dbContext) :
    IScoreStatisticRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<List<int>> GetStatisticTotalStudentsAsync()
    {
        var result = await _dbContext.Students
            .AsNoTracking() // Improve performance by not tracking changes
            .Include(s => s.ForeignLanguageCode)
            .Where(s => s.ForeignScore != null &&
                s.ForeignLanguageCode != null &&
                s.ForeignLanguageCode.ForeignCode == "N1")
            .GroupBy(s =>
                s.ForeignScore < 4 ? "Level 1" :
                s.ForeignScore < 6 ? "Level 2" :
                s.ForeignScore < 8 ? "Level 3" :
                "Level 4"
            )
            .Select(g => g.Count())
            .ToListAsync();
        return result;
    }
}