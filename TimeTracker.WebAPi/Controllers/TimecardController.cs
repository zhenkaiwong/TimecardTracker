using Microsoft.AspNetCore.Mvc;
using TimeTracker.WebAPi.Models;
using TimeTracker.WebAPi.Services.Data;
using TimeTracker.WebAPi.Services.Mapper;

namespace TimeTracker.WebAPi.Controllers;

[ApiController]
[Route("api/timecard")]
public class TimecardController
{

  protected IDataService<Timecard> _dataService;
  protected IDTOMapper<Timecard, TimecardRequest, TimecardResponse> _timecardMapper;

  public TimecardController(IDataService<Timecard> dataService, IDTOMapper<Timecard, TimecardRequest, TimecardResponse> mapper)
  {
    _dataService = dataService;
    _timecardMapper = mapper;
  }

  [HttpGet("{id}")]
  public async Task<TimecardResponse> GetTimecard(int id)
  {
    Timecard timecard = await _dataService.GetItemAsync(id);
    TimecardResponse response = _timecardMapper.MapToResponse(timecard);
    return response;
  }

  [HttpGet]
  public async Task<IEnumerable<TimecardResponse>> GetTimecards([FromQuery] Pagination pagination)
  {
    IEnumerable<Timecard> timecards = await _dataService.GetItemsAsync(pagination);
    IEnumerable<TimecardResponse> timecardResponses =
      timecards.Select(timecard => _timecardMapper.MapToResponse(timecard));

    return timecardResponses;
  }

  [HttpPost]
  public async Task<TimecardResponse> InsertTimecard(TimecardRequest request)
  {
    Timecard timecard = _timecardMapper.MapFromRequest(request);
    Timecard insertedTimecard = await _dataService.InsertItemAsync(timecard);
    TimecardResponse response = _timecardMapper.MapToResponse(insertedTimecard);
    return response;
  }

  [HttpPut("{id}")]
  public TimecardResponse UpdateTimecard(string id, TimecardRequest updatedTimecard)
  {
    throw new NotImplementedException();
  }

  [HttpDelete("{id}")]
  public BaseResponse RemoveTimecard(int id)
  {
    throw new NotImplementedException();
  }
}
