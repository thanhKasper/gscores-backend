using GScores.Core.Domains.Entities;
using GScores.Core.Domains.RepositoryContracts;
using GScores.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GScores.Infrastructure.Repositories;

public class GroupAScoreRankingRepository(ApplicationDbContext dbContext) : IGroupARankingRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public Task<List<Student>> GetTop10StudentsAsync()
    {
        var topStudents = _dbContext.Students
            .Where(s => s.IsNaturalScience == true &&
                s.MathScore != null &&
                s.PhysicsScore != null &&
                s.ChemistryScore != null)
            .Select(s => new Student
            {
                StudentId = s.StudentId,
                MathScore = s.MathScore,
                PhysicsScore = s.PhysicsScore,
                ChemistryScore = s.ChemistryScore,
                SumAGroupScore = s.MathScore + s.PhysicsScore + s.ChemistryScore
            })
            .OrderByDescending(s => s.SumAGroupScore)
            .Take(10);
        return topStudents.ToListAsync();
    }
}