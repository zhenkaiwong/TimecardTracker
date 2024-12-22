using TimeTracker.WebAPi.Models;

namespace TimeTracker.WebAPi.Services.Mapper;

public interface ITimecardTypeMapper
{
  TimecardType MapToTimecardType(string typeInString);
  string MapFromTimecardType(TimecardType type);
}
