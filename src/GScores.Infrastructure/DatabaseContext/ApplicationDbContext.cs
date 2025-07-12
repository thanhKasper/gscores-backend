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
        // Because the seeding is transactional, therefore if we 
        const int BATCH_SIZE = 100000;
        int currentCount = 0;
        HashSet<string> newForeignCodes = new();
        await foreach (var studentScore in _studentScoreReader.GetAllScoresAsync())
        {

            Student newStudent = studentScore.ToStudent();


            ForeignLanguageCode? langCode = null;

            if (!string.IsNullOrEmpty(studentScore.ForeignCode)) // When student register a foreign lang test
            {
                // When new foreign code is detected save change to database immediately
                if (!newForeignCodes.Contains(studentScore.ForeignCode))
                {
                    langCode = new ForeignLanguageCode
                    {
                        ForeignCode = studentScore.ForeignCode
                    };
                    context.Set<ForeignLanguageCode>().Add(langCode);
                    await context.SaveChangesAsync(); // Save changes immediately
                    newForeignCodes.Add(studentScore.ForeignCode);
                }
                else // The foreign code already exists
                {
                    langCode = await context.Set<ForeignLanguageCode>()
                        .FindAsync(studentScore.ForeignCode);
                }
            }


            newStudent.ForeignLanguageCode = langCode;

            context.Set<Student>().Add(newStudent);
            // Console.WriteLine("Completed adding one student");
            currentCount++;
            if (currentCount == BATCH_SIZE)
            {
                Console.WriteLine($"Saving batch of {BATCH_SIZE} students...");
                await context.SaveChangesAsync();
                context.ChangeTracker.Clear(); // Clear the change tracker to free up memory
                currentCount = 0; // Reset the counter after saving
            }
        }

        await context.SaveChangesAsync(); // In case the last batch is not a full batch
        context.ChangeTracker.Clear(); // Clear the change tracker after the final save

        Console.WriteLine("Seeding completed.");
        Console.WriteLine($"Total students seeded: {await context.Set<Student>().CountAsync()}");
        Console.WriteLine($"Total foreign language codes: {await context.Set<ForeignLanguageCode>().CountAsync()}");
    }
}