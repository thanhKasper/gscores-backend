using AutoFixture;
using FluentAssertions;
using GScores.Core.Domains.Entities;
using GScores.Core.Domains.RepositoryContracts;
using GScores.Core.DTOs;
using GScores.Core.ServiceContracts;
using GScores.Core.Services;
using Moq;

namespace GScores.UnitTest;

public class ReadScoreServiceTest
{
    private readonly IStudentRepository _studentRepo;
    private readonly IReadScoreService _readScoreService;
    private readonly IFixture _fixture;
    private readonly Mock<IStudentRepository> _studentRepoMock;
    public ReadScoreServiceTest()
    {
        _studentRepoMock = new();
        _studentRepo = _studentRepoMock.Object;
        _readScoreService = new ReadScoreService(_studentRepo);
        _fixture = new Fixture();
    }

    [Fact]
    public async Task GetScoreAsync_GetOneStudent_ReturnsResult()
    {
        var studentId = "01000001";
        var studentResult = _fixture.Build<Student>()
            .With(s => s.ForeignLanguageCode, null as ForeignLanguageCode)
            .Create();

        _studentRepoMock.Setup(repo => repo.FindStudentAsync(studentId))
            .ReturnsAsync(studentResult);

        var result = await _readScoreService.GetScoresAsync(studentId);

        result.Should().BeEquivalentTo(studentResult.ToStudentScore());
    }

    [Fact]
    public async Task GetScoreAsync_StudentIdIsNull_ThrowsArgumentNullException()
    {
        Func<Task> act = async () => await _readScoreService.GetScoresAsync(null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task GetScoreAsync_StudentIdIsEmpty_ThrowsArgumentNullException()
    {
        Func<Task> act = async () => await _readScoreService.GetScoresAsync(string.Empty);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task GetScoreAsync_StudentNotFound_ThrowsArgumentException()
    {
        var studentId = "01000001";
        _studentRepoMock.Setup(repo => repo.FindStudentAsync(studentId))
            .ReturnsAsync((Student?)null);

        Func<Task> act = async () => await _readScoreService.GetScoresAsync(studentId);
        await act.Should().ThrowAsync<ArgumentException>();
    }
}
