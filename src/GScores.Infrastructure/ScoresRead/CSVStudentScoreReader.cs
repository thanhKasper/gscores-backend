using System.Globalization;
using CsvHelper;
using GScores.Infrastructure.ScoresRead.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GScores.Infrastructure.ScoresRead;

public class CSVStudentScoreReader : IStudentScoreReader
{
    private readonly IConfiguration _configuration;
    private readonly StreamReader _reader;
    private readonly CsvReader _csvReader;

    public CSVStudentScoreReader(IConfiguration configuration)
    {
        _configuration = configuration;
        _reader = new StreamReader(_configuration["ScoresFilePath"]
            ?? throw new ArgumentNullException("ScoresFilePath is not found in configuration"));
        _csvReader = new CsvReader(_reader, CultureInfo.InvariantCulture);
        _csvReader.Context.RegisterClassMap<StudentScoreMap>();
    }


    public async Task<StudentScore?> GetOneLineAsync()
    {
        using (_reader)
        using (_csvReader)
        {
            _csvReader.Context.RegisterClassMap<StudentScoreMap>();

            if (!await _csvReader.ReadAsync())
                return null;

            return _csvReader.GetRecord<StudentScore>();
        }
    }

    public async IAsyncEnumerable<StudentScore> GetAllScoresAsync()
    {
        using (_reader)
        using (_csvReader)
        {
            _csvReader.Context.RegisterClassMap<StudentScoreMap>();
            int testCount = 1000;
            while (await _csvReader.ReadAsync())
            {
                if (testCount == 0)
                    yield break;
                var score = _csvReader.GetRecord<StudentScore>();
                if (score != null)
                {
                    yield return score;
                }
                testCount--;
            }

        }
    }
}
