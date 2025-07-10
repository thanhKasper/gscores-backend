using GScores.Core.Domains.RepositoryContracts;
using GScores.Core.RepositoryContracts;
using GScores.Infrastructure.DatabaseContext;

namespace GScores.Infrastructure.Repositories;

public class SubjectScoreStatisticRepositoryFactory(ApplicationDbContext dbContext) :
    IScoreStatisticRepositoryFactory
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public IScoreStatisticRepository CreateScoreStatisticRepository(string subject)
    {
        // Still wonder how to avoid this kind of if-else chain
        if (subject.Equals("Math", StringComparison.OrdinalIgnoreCase))
        {
            return new MathScoreStatisticRepository(_dbContext);
        }
        else if (subject.Equals("Literature", StringComparison.OrdinalIgnoreCase))
        {
            return new LiteratureScoreStatisticRepository(_dbContext);
        }
        else if (subject.Equals("Biology", StringComparison.OrdinalIgnoreCase))
        {
            return new BiologyScoreStatisticsRepository(_dbContext);
        }
        else if (subject.Equals("CivicEducation", StringComparison.OrdinalIgnoreCase))
        {
            return new CivicEducationScoreStatisticRepository(_dbContext);
        }
        else if (subject.Equals("Geography", StringComparison.OrdinalIgnoreCase))
        {
            return new GeographyScoreStatisticRepository(_dbContext);
        }
        else if (subject.Equals("History", StringComparison.OrdinalIgnoreCase))
        {
            return new HistoryScoreStatisticRepository(_dbContext);
        }
        else if (subject.Equals("Physics", StringComparison.OrdinalIgnoreCase))
        {
            return new PhysicsScoreStatisticRepository(_dbContext);
        }
        else if (subject.Equals("Chemistry", StringComparison.OrdinalIgnoreCase))
        {
            return new ChemistryScoreStatisticRepository(_dbContext);
        }
        else if (subject.Equals("English", StringComparison.OrdinalIgnoreCase))
        {
            return new EnglishScoreStatisticRepository(_dbContext);
        }
        else
        {
            throw new NotSupportedException($"Subject '{subject}' is not supported.");
        }
    }
}