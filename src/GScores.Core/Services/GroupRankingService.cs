using GScores.Core.Domains.RepositoryContracts;
using GScores.Core.DTOs;
using GScores.Core.ServiceContracts;

namespace GScores.Core.Services;

public class GroupRankingService(IGroupARankingRepository groupARankingRepository) : IGroupRankingService
{
    private readonly IGroupARankingRepository _groupARankingRepository = groupARankingRepository;

    public async Task<List<StudentGroupAScore>> GetTop10StudentsInAGroupAsync()
    {
        var res = await _groupARankingRepository.GetTop10StudentsAsync();
        return res.Select(s => s.ToStudentGroupAScore()).ToList();
    }
}