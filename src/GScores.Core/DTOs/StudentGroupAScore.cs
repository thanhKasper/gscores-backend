using GScores.Core.Domains.Entities;

namespace GScores.Core.DTOs;

public class StudentGroupAScore
{
    public string? StudentId { get; set; }
    public double? MathScore { get; set; }
    public double? PhysicsScore { get; set; }
    public double? ChemistryScore { get; set; }
    public double? SumAGroupScore { get; set; }
}

public static class StudentGroupAScoreExtensions
{
    public static StudentGroupAScore ToStudentGroupAScore(this Student student)
    {
        return new StudentGroupAScore
        {
            StudentId = student.StudentId,
            MathScore = student.MathScore,
            PhysicsScore = student.PhysicsScore,
            ChemistryScore = student.ChemistryScore,
            SumAGroupScore = student.SumAGroupScore
        };
    }
}