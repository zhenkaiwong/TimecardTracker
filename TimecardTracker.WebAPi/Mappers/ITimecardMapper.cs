using TimecardTracker.WebAPi.Models.Timecard;

namespace TimecardTracker.WebAPi.Mappers;

public interface ITimecardMapper
{

  Timecard MapFromRequest(TimecardRequest request);

  TimecardResponse MapToResponse(Timecard timecard);
  IEnumerable<TimecardResponse> MapEnumerableToResponses(IEnumerable<Timecard> timecards);
}
