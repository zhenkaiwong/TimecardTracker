using TimecardTracker.WebAPi.Exceptions;
using TimecardTracker.WebAPi.Models.Timecard;

namespace TimecardTracker.WebAPi.Helpers;

public class TimecardTypeHelpers
{
  public static TimecardType MapFromStringToTimecardType(string typeString)
  {
    switch (typeString)
    {
      case nameof(TimecardType.Activity):
        return TimecardType.Activity;
      case nameof(TimecardType.Ticket):
        return TimecardType.Ticket;
      default:
        throw new InvalidTimecardTypeException($"Unable to find time card type \"{typeString}\"");
    }
  }
}
