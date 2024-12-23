using System;
using System.ComponentModel.Design;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TimeTracker.WebAPi.Exceptions;
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
  public Task DeleteItemAsync(int id)
  {
    throw new NotImplementedException();
  }

  public async Task<Timecard> GetItemAsync(int id)
  {
    Timecard timecard = await _dbContext.Timecards.SingleAsync(timecard => timecard.Id.Equals(id));
    return timecard;
  }

  public async Task<IEnumerable<Timecard>> GetItemsAsync(Pagination pagination)
  {
    int skip = pagination.Skip;
    int rows = pagination.Rows == 0 ? 10 : pagination.Rows;
    string search = pagination.Search;

    IQueryable<Timecard> getQuery = _dbContext.Timecards
      .Where(timecard => timecard.FullTextSearch.Contains(search))
      .Take(rows)
      .Skip(skip);

    List<Timecard> timecards = await getQuery.ToListAsync();

    return timecards;
  }

  public async Task<Timecard> InsertItemAsync(Timecard item)
  {
    if (item.LastUpdated == default)
    {
      item.LastUpdated = DateTime.UtcNow;
    }

    if (string.IsNullOrEmpty(item.FullTextSearch))
    {
      item.FullTextSearch = BuildFullTextSearch(item);
    }

    await _dbContext.Timecards.AddAsync(item);
    await _dbContext.SaveChangesAsync();
    return item;
  }

  public Task<Timecard> UpdateItemAsync(int id, Timecard updatedItem)
  {
    throw new NotImplementedException();
  }

  protected virtual string BuildFullTextSearch(Timecard timecard)
  {
    StringBuilder stringToSearch = new();
    stringToSearch.Append(timecard.Subject);
    stringToSearch.Append(timecard.Comment);

    return stringToSearch.ToString();
  }
}
