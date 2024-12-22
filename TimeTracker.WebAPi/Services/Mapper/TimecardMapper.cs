using TimeTracker.WebAPi.Exceptions;
using TimeTracker.WebAPi.Models;

namespace TimeTracker.WebAPi.Services.Mapper;

public class TimecardMapper : IDTOMapper<Timecard, TimecardRequest, TimecardResponse>
{
  protected ITimecardTypeMapper _typeMapper;
  public TimecardMapper(ITimecardTypeMapper mapper)
  {
    _typeMapper = mapper;
  }

  public Timecard MapFromRequest(TimecardRequest request)
  {
    if (string.IsNullOrEmpty(request.Type))
    {
      throw new InvalidTimecardTypeException("Unable to find timecard type from request");
    }

    TimecardType type = _typeMapper.MapToTimecardType(request.Type);

    return new Timecard()
    {
      Id = null,
      Type = type,
      Subject = request.Subject ?? string.Empty,
      Comment = request.Comment ?? string.Empty,
      StartTime = request.StartTime,
      EndTime = request.EndTime,
      Created = request.Created
    };
  }

  public TimecardResponse MapToResponse(Timecard source)
  {
    string typeInString = _typeMapper.MapFromTimecardType(source.Type);
    return new TimecardResponse()
    {
      Id = source.Id,
      Type = typeInString,
      Subject = source.Subject ?? string.Empty,
      Comment = source.Comment,
      StartTime = source.StartTime,
      EndTime = source.EndTime,
      LastUpdated = source.LastUpdated,
      Created = source.Created,
    };
  }
}
