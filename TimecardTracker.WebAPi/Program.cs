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

builder.Services.AddDbContext<TimecardDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("db")));

builder.Services.AddSingleton<ITimecardMapper, TimecardMapper>();
builder.Services.AddScoped<ITimecardDataService, TimecardDataService>();

var app = builder.Build();

string? dbConnectionString = app.Configuration.GetConnectionString("db");

if (string.IsNullOrEmpty(dbConnectionString) || !dbConnectionString.StartsWith("Data Source="))
{
    Console.WriteLine("Unable to start application. You need to ensure the db connection string is valid");
    return;
}

string dbFilePath = dbConnectionString.Substring(12);
Console.WriteLine($"db file path: {dbFilePath}");

if (!File.Exists(dbFilePath))
{
    Console.WriteLine($"Unable to find sqlite db file in the specified location from Connection String. The location is: {dbFilePath}");
    Console.WriteLine("You can create this file by running ef db update command");
    return;
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
