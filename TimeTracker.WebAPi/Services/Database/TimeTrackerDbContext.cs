using System;
using Microsoft.EntityFrameworkCore;
using TimeTracker.WebAPi.Models;

namespace TimeTracker.WebAPi.Services.Database;

public class TimeTrackerDbContext : DbContext
{
  public TimeTrackerDbContext(DbContextOptions<TimeTrackerDbContext> options) : base(options)
  {

  }
  public DbSet<Timecard> Timecards { get; set; }
}
