using GScores.Core.Domains;
using GScores.Core.Domains.RepositoryContracts;
using GScores.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GScores.Infrastructure.Repositories;

public class StudentRepository(ApplicationDbContext dbContext) : IStudentRepository
{
    private readonly ApplicationDbContext _context = dbContext;

    public async Task<Student?> FindStudentAsync(string studentId)
    {
        return await _context.Students
            .AsNoTracking()
            .Include(s => s.ForeignLanguageCode)
            .FirstOrDefaultAsync(s => s.StudentId == studentId);
    }
}