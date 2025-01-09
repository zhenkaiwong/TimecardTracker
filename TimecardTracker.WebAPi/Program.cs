using Microsoft.EntityFrameworkCore;
using TimecardTracker.WebAPi.Mappers;
using TimecardTracker.WebAPi.Models;
using TimecardTracker.WebAPi.Models.Timecard;
using TimecardTracker.WebAPi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TimecardDbContext>(options => options.UseInMemoryDatabase("timecards"));

builder.Services.AddSingleton<ITimecardMapper, TimecardMapper>();
builder.Services.AddScoped<ITimecardDataService, TimecardDataService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    using (IServiceScope scope = app.Services.CreateScope())
    {
        TimecardDbContext db = scope.ServiceProvider.GetRequiredService<TimecardDbContext>();
        DbSeeder.Seed(db);
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

class DbSeeder
{
    private readonly static List<Timecard> dataset = new() {
        new Timecard(TimecardType.Activity, "Post base course", "Containers", new TimeOnly(13, 0), new TimeOnly(15, 30), new DateOnly(2024, 12, 22)),
        new Timecard(TimecardType.Ticket, "CS12345", "Review ticket", new TimeOnly(12, 30), new TimeOnly(13, 00), new DateOnly(2024, 11, 21)),
        new Timecard(TimecardType.Ticket, "CS12346", "Reproduce issue", new TimeOnly(10, 15), new TimeOnly(11, 30), new DateOnly(2024, 9, 30)),
    };

    public static void Seed(TimecardDbContext dbContext)
    {
        dbContext.Timecards.AddRange(dataset);
        dbContext.SaveChanges();
    }
}
