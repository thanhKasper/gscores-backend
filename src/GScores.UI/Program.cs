using GScores.Core.Domains.RepositoryContracts;
using GScores.Core.RepositoryContracts;
using GScores.Core.ServiceContracts;
using GScores.Core.Services;
using GScores.Infrastructure.DatabaseContext;
using GScores.Infrastructure.Repositories;
using GScores.Infrastructure.ScoresRead;
using GScores.Infrastructure.ScoresRead.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IStudentScoreReader, CSVStudentScoreReader>();
builder.Services.AddScoped<IReadScoreService, ReadScoreService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IScoreStatisticRepositoryFactory, SubjectScoreStatisticRepositoryFactory>();
builder.Services.AddScoped<IScoreStatistic, ScoreStatistics>();
builder.Services.AddScoped<IGroupARankingRepository, GroupAScoreRankingRepository>();
builder.Services.AddScoped<IGroupRankingService, GroupRankingService>();
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new ArgumentNullException("DefaultConnection is not found in configuration"));
});

var app = builder.Build();

app.UseCors(options =>
{
    options.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});
app.UseRouting();
app.MapControllers();
app.Run();
