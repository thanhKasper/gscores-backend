namespace GScores.Core.ServiceContracts;

public interface IScoreStatistic
{
    Task<List<int>> GetStatisticTotalStudentsOfSubjectAsync(string subject);
}