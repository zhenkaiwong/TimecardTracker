using System;
using Microsoft.EntityFrameworkCore;
using TimeTracker.WebAPi.Models;
using TimeTracker.WebAPi.Services.Database;

namespace TimeTracker.WebAPi.Services.Data;

public class TimecardDataService : IDataService<Timecard>
{

  protected TimeTrackerDbContext _dbContext;

  public TimecardDataService(TimeTrackerDbContext dbContext)
  {
    _dbContext = dbContext;
  }
  public Task DeleteItem(int id)
  {
    throw new NotImplementedException();
  }

  public async Task<Timecard> GetItem(int id)
  {
    Timecard timecard = await _dbContext.Timecards.SingleAsync(timecard => timecard.Id.Equals(id));
    return timecard;
  }

  public Task<IEnumerable<Timecard>> GetItems(Pagination pagination)
  {
    throw new NotImplementedException();
  }

  public async Task<Timecard> InsertItem(Timecard item)
  {
    await _dbContext.Timecards.AddAsync(item);
    return item;
  }

  public Task<Timecard> UpdateItem(int id, Timecard updatedItem)
  {
    throw new NotImplementedException();
  }
}
