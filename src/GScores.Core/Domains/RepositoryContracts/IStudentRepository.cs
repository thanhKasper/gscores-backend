using GScores.Core.Domains.Entities;

namespace GScores.Core.Domains.RepositoryContracts;

public interface IStudentRepository
{
    /// <summary>
    /// Finds a student by their ID.
    /// </summary>
    /// <param name="studentId">The ID of the student to find.</param>
    /// <returns>A <see cref="Student"/> object if found, otherwise null.</returns>
    Task<Student?> FindStudentAsync(string studentId);
}