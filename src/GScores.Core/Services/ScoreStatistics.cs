using GScores.Core.Domains.RepositoryContracts;
using GScores.Core.RepositoryContracts;
using GScores.Core.ServiceContracts;

namespace GScores.Core.Services;

// Factory Method Pattern
public class ScoreStatistics(IScoreStatisticRepositoryFactory factory) : IScoreStatistic
{
    private readonly IScoreStatisticRepositoryFactory _scoreStatisticRepositoryFactory = factory;

    public Task<List<int>> GetStatisticTotalStudentsOfSubjectAsync(string subject)
    {
        IScoreStatisticRepository repository = CreateScoreStatisticRepository(subject);
        return repository.GetStatisticTotalStudentsAsync();
    }

    public IScoreStatisticRepository CreateScoreStatisticRepository(string subject)
    {
        return _scoreStatisticRepositoryFactory.CreateScoreStatisticRepository(subject);
    }
}