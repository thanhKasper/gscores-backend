using GScores.Core.Domains.RepositoryContracts;
using GScores.Core.RepositoryContracts;
using GScores.Core.ServiceContracts;

namespace GScores.Core.Services;

// Factory Method Pattern
public class ScoreStatistics(IScoreStatisticRepositoryFactory factory) : IScoreStatistic
{
    private readonly IScoreStatisticRepositoryFactory _scoreStatisticRepositoryFactory = factory;

    public Task<List<int>> GetStatisticTotalStudentsOfSubjectAsync(string? subject)
    {  
        if (string.IsNullOrEmpty(subject))
        {
            throw new ArgumentNullException(nameof(subject), "Subject cannot be null or empty.");
        }

        List<string> VALID_SUBJECTS = new()
        {
            "MATH", "LITERATURE", "ENGLISH",
            "PHYSICS", "CHEMISTRY", "BIOLOGY", "HISTORY", "GEOGRAPHY", "CIVICEDUCATION"
        };
        
        if (!VALID_SUBJECTS.Contains(subject.ToUpper()))
        {
            throw new ArgumentException($"Invalid subject: {subject}.");
        }

        IScoreStatisticRepository repository = CreateScoreStatisticRepository(subject);
        return repository.GetStatisticTotalStudentsAsync();
    }

    public IScoreStatisticRepository CreateScoreStatisticRepository(string subject)
    {
        return _scoreStatisticRepositoryFactory.CreateScoreStatisticRepository(subject);
    }
}