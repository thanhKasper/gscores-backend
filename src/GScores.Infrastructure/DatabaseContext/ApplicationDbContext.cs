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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSeeding((context, _) =>
        {
            SeedDataAsync(context).Wait();
        });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public async Task SeedDataAsync(DbContext context)
    {
        await foreach (var studentScore in _studentScoreReader.GetAllScoresAsync())
        {
            // Check if student already exists
            if (await context.Set<Student>().AnyAsync(s => s.StudentId == studentScore.StudentId))
                continue;

            Student newStudent = studentScore.ToStudent();

            var existingForeignCode = await context.Set<ForeignLanguageCode>()
                .FirstOrDefaultAsync(f => f.ForeignCode == studentScore.ForeignCode);

            if (!string.IsNullOrEmpty(studentScore.ForeignCode))
            {

                if (existingForeignCode == null)
                {
                    existingForeignCode = new ForeignLanguageCode
                    {
                        ForeignCode = studentScore.ForeignCode
                    };
                    context.Set<ForeignLanguageCode>().Add(existingForeignCode);
                }

            }


            newStudent.ForeignLanguageCode = existingForeignCode;

            context.Set<Student>().Add(newStudent);
            await context.SaveChangesAsync();
        }

    }
}