using System.Threading.Tasks;
using GScores.Core.Domains;
using GScores.Infrastructure.ScoresRead.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GScores.Infrastructure.DatabaseContext;

public class ApplicationDbContext(
    DbContextOptions options,
    IStudentScoreReader studentScoreReader) : DbContext(options)
{
    private readonly IStudentScoreReader _studentScoreReader = studentScoreReader;
    public virtual DbSet<Student> Students { get; set; }
    public virtual DbSet<ForeignLanguageCode> ForeignLanguageCodes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        Task.Run(async () =>
        {
            await foreach (var studentScore in _studentScoreReader.GetAllScoresAsync())
            {
                HashSet<string> foreignCodes = new();
                Student newStudent = new()
                {
                    StudentId = studentScore.StudentId,
                    MathScore = (decimal?)studentScore.MathScore,
                    ForeignScore = (decimal?)studentScore.ForeignScore,
                    LiteratureScore = (decimal?)studentScore.LiteratureScore,
                    PhysicsScore = (decimal?)studentScore.PhysicsScore,
                    ChemistryScore = (decimal?)studentScore.ChemistryScore,
                    BiologyScore = (decimal?)studentScore.BiologyScore,
                    HistoryScore = (decimal?)studentScore.HistoryScore,
                    GeographyScore = (decimal?)studentScore.GeographyScore,
                    CivicEducationScore = (decimal?)studentScore.CivicEducationScore,
                };

                newStudent.IsNaturalScience = studentScore.HistoryScore == null &&
                                        studentScore.GeographyScore == null &&
                                        studentScore.CivicEducationScore == null;

                if (studentScore.ForeignCode != null)
                {
                    if (foreignCodes.Contains(studentScore.ForeignCode))
                    {
                        ForeignLanguageCode? existingForeignCode = await ForeignLanguageCodes
                            .FirstOrDefaultAsync(f => f.ForeignCode == studentScore.ForeignCode);
                        newStudent.ForeignLanguageCode = existingForeignCode;
                    }
                    else
                    {
                        foreignCodes.Add(studentScore.ForeignCode);
                        ForeignLanguageCode newForeignCode = new()
                        {
                            ForeignCode = studentScore.ForeignCode
                        };
                        ForeignLanguageCodes.Add(newForeignCode);
                        newStudent.ForeignLanguageCode = newForeignCode;
                        modelBuilder.Entity<ForeignLanguageCode>().HasData(newForeignCode);
                    }
                }

                modelBuilder.Entity<Student>().HasData(newStudent);
            }
        });
    }
}