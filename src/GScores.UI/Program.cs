using GScores.Infrastructure.DatabaseContext;
using GScores.Infrastructure.ScoresRead;
using GScores.Infrastructure.ScoresRead.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IStudentScoreReader, CSVStudentScoreReader>();
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new ArgumentNullException("DefaultConnection is not found in configuration"));
});

var app = builder.Build();

app.MapControllers();
app.Run();
