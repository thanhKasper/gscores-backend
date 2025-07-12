using GScores.Core.Domains.RepositoryContracts;
using GScores.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GScores.Infrastructure.Repositories;

public class GeographyScoreStatisticRepository(ApplicationDbContext dbContext) : IScoreStatisticRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<List<int>> GetStatisticTotalStudentsAsync()
    {
        var result = await _dbContext.Students
            .AsNoTracking()
            .Where(s => s.GeographyScore != null && s.IsNaturalScience == false)
            .GroupBy(s =>
                s.GeographyScore < 4 ? "Level 1" :
                s.GeographyScore < 6 ? "Level 2" :
                s.GeographyScore < 8 ? "Level 3" :
                "Level 4"
            )
            .Select(g => g.Count())
            .ToListAsync();
        return result;
    }
}