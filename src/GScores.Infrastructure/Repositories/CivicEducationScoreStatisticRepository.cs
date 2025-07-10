using GScores.Core.Domains.RepositoryContracts;
using GScores.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GScores.Infrastructure.Repositories;

public class CivicEducationScoreStatisticRepository(ApplicationDbContext dbContext) :
    IScoreStatisticRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    
    public async Task<List<int>> GetStatisticTotalStudentsAsync()
    {
        var result = await _dbContext.Students
            .AsNoTracking()
            .Where(s => s.CivicEducationScore != null && s.IsNaturalScience == false)
            .GroupBy(s =>
                s.CivicEducationScore < 4 ? "Level 1" :
                s.CivicEducationScore < 6 ? "Level 2" :
                s.CivicEducationScore < 8 ? "Level 3" :
                "Level 4"
            )
            .Select(g => g.Count())
            .ToListAsync();
        return result;
    }
}