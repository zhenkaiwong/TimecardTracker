using TimeTracker.WebAPi.Exceptions;
using TimeTracker.WebAPi.Models;

namespace TimeTracker.WebAPi.Services.Mapper;

public class TimecardTypeMapper : ITimecardTypeMapper
{
  public string MapFromTimecardType(TimecardType type)
  {
    switch (type)
    {
      case TimecardType.TICKET:
        return "TICKET";
      case TimecardType.ACTIVITY:
        return "ACTIVITY";
      default:
        throw new InvalidTimecardTypeException($"Invalid timecard type: {type.ToString()}");
    }
  }

  public TimecardType MapToTimecardType(string typeInString)
  {
    switch (typeInString)
    {
      case "TICKET":
        return TimecardType.TICKET;
      case "ACTIVITY":
        return TimecardType.ACTIVITY;
      default:
        throw new InvalidTimecardTypeException($"Invalid timecard type: {typeInString}");
    }
  }
}
