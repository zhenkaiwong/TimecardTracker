using Microsoft.EntityFrameworkCore;
using TimeTracker.WebAPi;
using TimeTracker.WebAPi.Models;
using TimeTracker.WebAPi.Services.Data;
using TimeTracker.WebAPi.Services.Database;
using TimeTracker.WebAPi.Services.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ITimecardTypeMapper, TimecardTypeMapper>();
builder.Services.AddSingleton<IDTOMapper<Timecard, TimecardRequest, TimecardResponse>, TimecardMapper>();
builder.Services.AddTransient<IDataService<Timecard>, TimecardDataService>();

builder.Services.AddDbContext<TimeTrackerDbContext>(options => options.UseInMemoryDatabase("timetrackerdb"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    using (IServiceScope scope = app.Services.CreateScope())
    {
        TimeTrackerDbContext? context = scope.ServiceProvider.GetService<TimeTrackerDbContext>();
        if (context is null)
        {
            throw new InvalidOperationException("Unable to resolve dbcontext from services");
        }
        Db.Seed(context);
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
