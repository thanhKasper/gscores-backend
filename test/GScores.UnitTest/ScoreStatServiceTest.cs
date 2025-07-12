using FluentAssertions;
using GScores.Core.Domains.RepositoryContracts;
using GScores.Core.RepositoryContracts;
using GScores.Core.ServiceContracts;
using GScores.Core.Services;
using Moq;

namespace GScores.UnitTest;

public class ScoreStatisticsServiceTest
{
    private readonly IScoreStatistic _scoreStatisticsService;
    private readonly IScoreStatisticRepositoryFactory _repositoryFactory;
    private readonly Mock<IScoreStatisticRepositoryFactory> _repositoryFactoryMock;

    public ScoreStatisticsServiceTest()
    {
        _repositoryFactoryMock = new Mock<IScoreStatisticRepositoryFactory>();
        _repositoryFactory = _repositoryFactoryMock.Object;
        _scoreStatisticsService = new ScoreStatistics(_repositoryFactory);
    }

    // Add your test methods here
    [Fact]
    public async Task GetStatisticTotalStudentsOfSubjectAsync_ValidSubject_ReturnsExpectedResult()
    {
        // Arrange
        string subject = "math";
        var expectedResult = new List<int> { 1, 2, 3 };
        var repositoryMock = new Mock<IScoreStatisticRepository>();
        _repositoryFactoryMock.Setup(f => f.CreateScoreStatisticRepository(subject))
            .Returns(repositoryMock.Object);
        repositoryMock.Setup(r => r.GetStatisticTotalStudentsAsync())
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _scoreStatisticsService.GetStatisticTotalStudentsOfSubjectAsync(subject);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public async Task GetStatisticTotalStudentsOfSubjectAsync_InvalidSubject_ThrowsArgumentException()
    {
        // Arrange
        string subject = "invalid_subject";

        var action = async () => await _scoreStatisticsService.GetStatisticTotalStudentsOfSubjectAsync(subject);

        await action.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task GetStatisticTotalStudentsOfSubjectAsync_NullOrEmptySubject_ThrowsArgumentNullException()
    {
        // Arrange
        string? subject = null;

        var action = async () => await _scoreStatisticsService.GetStatisticTotalStudentsOfSubjectAsync(subject);

        await action.Should().ThrowAsync<ArgumentNullException>();
    }
}