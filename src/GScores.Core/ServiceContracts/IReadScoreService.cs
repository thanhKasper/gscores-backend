using GScores.Core.DTOs;

namespace GScores.Core.ServiceContracts;

public interface IReadScoreService
{
    /// <summary>
    /// Retrieves scores for a specific student.
    /// </summary>
    /// <param name="studentId">The ID of the student whose scores are to be retrieved.</param>
    /// <returns>A Score from <see cref="StudentScore"/>
    /// <para>If student is not participating in the Natural Science track, the scores for Physics, Chemistry, and Biology will be null.</para>
    /// <para>If student is not participating in the Social Science track, the scores for History, Geography, and Civic Education will be null.</para>
    /// </returns>
    Task<StudentScore> GetScoresAsync(string? studentId);
}