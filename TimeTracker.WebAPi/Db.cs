using System;
using Microsoft.EntityFrameworkCore;
using TimeTracker.WebAPi.Models;
using TimeTracker.WebAPi.Services.Converter;
using TimeTracker.WebAPi.Services.Database;

namespace TimeTracker.WebAPi;

public static class Db
{

  private static Timecard CreateDummyTimecard(int id, TimecardType type, string subject, string comment, string startTime, string endTime, string created)
  {
    return new Timecard()
    {
      Id = id,
      Type = type,
      Subject = subject,
      Comment = comment,
      StartTime = startTime,
      EndTime = endTime,
      Created = DateConverter.ConvertRawDateToDateOnly(created),
      LastUpdated = DateTime.UtcNow,
      FullTextSearch = $"{subject} {comment}"
    };
  }
  private static List<Timecard> DummyTimecards = new() {
    CreateDummyTimecard(1, TimecardType.TICKET, "CS10001", "Investigate issue", "10:00 AM", "10:30 AM", "12/22/2024"),
    CreateDummyTimecard(2, TimecardType.TICKET, "CS10002", "Investigate issue", "1:00 PM", "3:30 PM", "12/24/2024"),
    CreateDummyTimecard(3, TimecardType.TICKET, "CS10003", "Investigate issue", "4:30 PM", "5:45 PM", "12/25/2024"),
    CreateDummyTimecard(4, TimecardType.ACTIVITY, "Base course", "Review web course material", "10:00 AM", "12:30 PM", "12/22/2024"),
    CreateDummyTimecard(5, TimecardType.ACTIVITY, "Post base course", "Investigate issue", "11:00 AM", "1:00 PM", "12/23/2024"),
  };
  public static void Seed(TimeTrackerDbContext context)
  {
    DbSet<Timecard> timecards = context.Timecards;

    if (timecards.Any())
    {
      return;
    }

    timecards.AddRange(DummyTimecards);
    context.SaveChanges();
  }
}
