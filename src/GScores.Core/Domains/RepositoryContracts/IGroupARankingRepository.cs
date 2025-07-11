using GScores.Core.Domains.Entities;

namespace GScores.Core.Domains.RepositoryContracts;

public interface IGroupARankingRepository
{
    Task<List<Student>> GetTop10StudentsAsync();
}