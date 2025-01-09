using TimecardTracker.WebAPi.Models;
using TimecardTracker.WebAPi.Models.Timecard;

namespace TimecardTracker.WebAPi.Services;

public interface ITimecardDataService
{
  Task<Timecard> InsertTimecardAsync(Timecard timecardToInsert);
  Task<Timecard> UpdateTimecardAsync(Timecard oldTimecard, Timecard newTimecard);
  Task RemoveTimecardByIdAsync(int id);
  Task<Timecard?> GetSingleTimecardByIdAsync(int id);
  Task<IEnumerable<Timecard>> GetTimecardsAsync(Pagination pagination);
}
