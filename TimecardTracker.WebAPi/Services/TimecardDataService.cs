using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimecardTracker.WebAPi.Exceptions;
using TimecardTracker.WebAPi.Helpers;
using TimecardTracker.WebAPi.Models;
using TimecardTracker.WebAPi.Models.Timecard;

namespace TimecardTracker.WebAPi.Services;

public class TimecardDataService : ITimecardDataService
{
  private readonly TimecardDbContext _db;
  public TimecardDataService(TimecardDbContext db)
  {
    _db = db;
  }
  public async Task<Timecard?> GetSingleTimecardByIdAsync(int id)
  {
    Timecard? timecard = await _db.Timecards.SingleOrDefaultAsync(timecard => timecard.Id.Equals(id));

    return timecard;
  }

  public async Task<IEnumerable<Timecard>> GetTimecardsAsync(Pagination pagination)
  {
    int take = (!pagination.Take.Equals(default)) ? pagination.Take : 5;
    int skip = (!pagination.Skip.Equals(default)) ? pagination.Skip : 0;

    IQueryable<Timecard> getTimecardsQuery = _db.Timecards;

    if (!string.IsNullOrEmpty(pagination.WeekStartDate))
    {
      DateOnly weekStartDate = DateAndTimeHelpers.MapFromDateString(pagination.WeekStartDate);
      DateOnly weekEndDate = weekStartDate.AddDays(7);
      getTimecardsQuery = getTimecardsQuery.Where(timecard => timecard.Created >= weekStartDate && timecard.Created < weekEndDate);
    }

    if (!string.IsNullOrEmpty(pagination.Type))
    {
      TimecardType ticketType = TimecardTypeHelpers.MapFromStringToTimecardType(pagination.Type);
      getTimecardsQuery = getTimecardsQuery.Where(timecard => timecard.Type.Equals(ticketType));
    }

    if (!string.IsNullOrEmpty(pagination.Description))
    {
      getTimecardsQuery = getTimecardsQuery.Where(timecard => timecard.Description.Contains(pagination.Description));
    }

    getTimecardsQuery = getTimecardsQuery.Skip(skip).Take(take);

    IEnumerable<Timecard> timecards = await getTimecardsQuery.ToListAsync();

    return timecards;
  }

  public async Task<Timecard> InsertTimecardAsync(Timecard timecardToInsert)
  {
    await _db.Timecards.AddAsync(timecardToInsert);
    await _db.SaveChangesAsync();
    return timecardToInsert;
  }

  public async Task RemoveTimecardByIdAsync(int id)
  {
    Timecard? timecardToRemove = await GetSingleTimecardByIdAsync(id);
    if (timecardToRemove is null)
    {
      throw new NotFoundException($"Unable to locate timecard with ID \"{id}\"");
    }

    _db.Timecards.Remove(timecardToRemove);
    await _db.SaveChangesAsync();
  }

  public async Task<Timecard> UpdateTimecardAsync(Timecard oldTimecard, Timecard newTimecard)
  {
    oldTimecard.UpdateValue(newTimecard);
    await _db.SaveChangesAsync();

    return newTimecard;
  }
}
