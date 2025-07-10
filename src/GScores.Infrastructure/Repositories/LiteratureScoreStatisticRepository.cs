using GScores.Core.Domains.RepositoryContracts;
using GScores.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GScores.Infrastructure.Repositories;

public class LiteratureScoreStatisticRepository(ApplicationDbContext dbContext) : IScoreStatisticRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<List<int>> GetStatisticTotalStudentsAsync()
    {
        var result = await _dbContext.Students
            .AsNoTracking() // Improve performance by not tracking changes
            .Where(s => s.LiteratureScore != null)
            .GroupBy(s =>
                s.LiteratureScore < 4 ? "Level 1" :
                s.LiteratureScore < 6 ? "Level 2" :
                s.LiteratureScore < 8 ? "Level 3" :
                "Level 4"
            )
            .Select(g => g.Count())
            .ToListAsync();
        return result;
    }
}