namespace GScores.Core.Domains.RepositoryContracts;

public interface IScoreStatisticRepository
{
    Task<List<int>> GetStatisticTotalStudentsAsync();
}