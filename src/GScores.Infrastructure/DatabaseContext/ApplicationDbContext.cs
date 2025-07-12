using GScores.Core.Domains.Entities;
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
        const int BATCH_SIZE = 100000;
        int currentCount = 0;
        HashSet<string> newForeignCodes =
            context.Set<ForeignLanguageCode>().Select(f => f.ForeignCode!).ToHashSet();
        await foreach (var studentScore in _studentScoreReader.GetAllScoresAsync())
        {
            // Check if student already exists
            if (await context.Set<Student>().AnyAsync(s => s.StudentId == studentScore.StudentId))
                continue;

            Student newStudent = studentScore.ToStudent();

            var existingForeignCode = await context.Set<ForeignLanguageCode>()
                .FirstOrDefaultAsync(f => f.ForeignCode == studentScore.ForeignCode);

            if (!string.IsNullOrEmpty(studentScore.ForeignCode)) // When student register a foreign lang test
            {
                // When new foreign code is detected save change to database immediately
                if (existingForeignCode == null &&
                    !newForeignCodes.Contains(studentScore.ForeignCode))
                {
                    existingForeignCode = new ForeignLanguageCode
                    {
                        ForeignCode = studentScore.ForeignCode
                    };
                    context.Set<ForeignLanguageCode>().Add(existingForeignCode);
                    newForeignCodes.Add(studentScore.ForeignCode);
                    newStudent.ForeignLanguageCode = existingForeignCode;
                    context.Set<Student>().Add(newStudent);
                    await context.SaveChangesAsync(); // Save changes immediately
                    currentCount = 0; // Reset the counter after saving
                    continue; // Skip to the next student
                }

            }


            newStudent.ForeignLanguageCode = existingForeignCode;

            context.Set<Student>().Add(newStudent);

            currentCount++;
            if (currentCount == BATCH_SIZE)
            {
                await context.SaveChangesAsync();
                currentCount = 0; // Reset the counter after saving
            }
        }

        await context.SaveChangesAsync(); // In case the last batch is not a full batch
    }
}