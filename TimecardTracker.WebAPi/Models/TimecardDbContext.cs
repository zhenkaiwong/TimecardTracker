using Microsoft.EntityFrameworkCore;

namespace TimecardTracker.WebAPi.Models;

public class TimecardDbContext : DbContext
{
  public DbSet<Timecard.Timecard> Timecards { get; set; }
  public TimecardDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
  {
  }
}
