using GScores.Core.Domains;

namespace GScores.Core.DTOs;

public class StudentScore
{
    public string? StudentId { get; set; }
    public double? MathScore { get; set; }
    public double? LiteratureScore { get; set; }
    public double? ForeignLanguageScore { get; set; }
    public double? HistoryScore { get; set; }
    public double? GeographyScore { get; set; }
    public double? CivicEducationScore { get; set; }
    public double? BiologyScore { get; set; }
    public double? ChemistryScore { get; set; }
    public double? PhysicsScore { get; set; }
    public string? LangCode { get; set; }
}

public static class StudentClassExtension
{
    public static StudentScore ToStudentScore(this Student student)
    {
        return new StudentScore
        {
            StudentId = student.StudentId,
            MathScore = student.MathScore,
            LiteratureScore = student.LiteratureScore,
            ForeignLanguageScore = student.ForeignScore,
            HistoryScore = student.HistoryScore,
            GeographyScore = student.GeographyScore,
            CivicEducationScore = student.CivicEducationScore,
            BiologyScore = student.BiologyScore,
            ChemistryScore = student.ChemistryScore,
            PhysicsScore = student.PhysicsScore,
            LangCode = student.ForeignLanguageCode?.ForeignCode,
        };
    }
}
