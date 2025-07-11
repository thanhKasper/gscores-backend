using GScores.Core.DTOs;

namespace GScores.Core.ServiceContracts;

public interface IGroupRankingService
{
    /// <summary>
    /// Get the top 10 students in A group (Natural Science) based on their scores.
    /// The A group includes Math, Physics, and Chemistry subjects.
    /// Students must have scores in all three subjects to be considered.
    /// </summary>
    /// <returns>A list of top 10 students who achieve the highest scores in A group.</returns>
    Task<List<StudentGroupAScore>> GetTop10StudentsInAGroupAsync();
}