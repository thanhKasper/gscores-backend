using GScores.Core.Domains.RepositoryContracts;

namespace GScores.Core.RepositoryContracts;

// Abstract Factory Pattern
public interface IScoreStatisticRepositoryFactory
{
    IScoreStatisticRepository CreateScoreStatisticRepository(string subject);
}