namespace GScores.Infrastructure.ScoresRead.Interfaces;

public interface IStudentScoreReader
{
    Task<StudentScore?> GetOneLineAsync();
    IAsyncEnumerable<StudentScore> GetAllScoresAsync();
}
