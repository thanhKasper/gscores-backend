using GScores.Core.Domains.RepositoryContracts;
using GScores.Core.DTOs;
using GScores.Core.ServiceContracts;

namespace GScores.Core.Services;

public class ReadScoreService(IStudentRepository studentRepository) : IReadScoreService
{
    private readonly IStudentRepository _studentRepository = studentRepository;

    public async Task<StudentScore> GetScoresAsync(string? studentId)
    {
        if (string.IsNullOrEmpty(studentId))
        {
            throw new ArgumentNullException(nameof(studentId), "Student ID cannot be null or empty.");
        }


        var student = await _studentRepository.FindStudentAsync(studentId)
            ?? throw new ArgumentException($"Student with ID {studentId} not found.", nameof(studentId));

        var studentDto = student.ToStudentScore();
        if (student.IsNaturalScience!.Value)
        {
            studentDto.PhysicsScore ??= 0.0;
            studentDto.ChemistryScore ??= 0.0;
            studentDto.BiologyScore ??= 0.0;
        }
        else
        {
            studentDto.HistoryScore ??= 0.0;
            studentDto.GeographyScore ??= 0.0;
            studentDto.CivicEducationScore ??= 0.0;
        }

        return studentDto;
    }
}