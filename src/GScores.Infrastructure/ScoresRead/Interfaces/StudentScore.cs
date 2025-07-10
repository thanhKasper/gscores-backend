using GScores.Core.Domains;

namespace GScores.Infrastructure.ScoresRead.Interfaces;

public class StudentScore
{
    public string? StudentId { get; set; }
    public double? MathScore { get; set; }
    public double? LiteratureScore { get; set; }
    public double? ForeignScore { get; set; }
    public double? PhysicsScore { get; set; }
    public double? ChemistryScore { get; set; }
    public double? BiologyScore { get; set; }
    public double? HistoryScore { get; set; }
    public double? GeographyScore { get; set; }
    public double? CivicEducationScore { get; set; }
    public string? ForeignCode { get; set; }

    public Student ToStudent()
    {
        return new Student
        {
            StudentId = StudentId,
            MathScore = MathScore,
            LiteratureScore = LiteratureScore,
            ForeignScore = ForeignScore,
            PhysicsScore = PhysicsScore,
            ChemistryScore = ChemistryScore,
            BiologyScore = BiologyScore,
            HistoryScore = HistoryScore,
            GeographyScore = GeographyScore,
            CivicEducationScore = CivicEducationScore,
            IsNaturalScience = HistoryScore == null && GeographyScore == null && CivicEducationScore == null
        };
    }
}