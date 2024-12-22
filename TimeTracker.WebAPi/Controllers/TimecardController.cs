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

  [HttpGet("/{id}")]
  public TimecardResponse GetTimecard(int id)
  {
    throw new NotImplementedException();
  }

  [HttpGet]
  public IEnumerable<TimecardResponse> GetTimecard(Pagination pagination)
  {
    throw new NotImplementedException();
  }

  [HttpPost]
  public async Task<TimecardResponse> InsertTimecard(TimecardRequest request)
  {
    Timecard timecard = _timecardMapper.MapFromRequest(request);
    Timecard insertedTimecard = await _dataService.InsertItem(timecard);
    TimecardResponse response = _timecardMapper.MapToResponse(insertedTimecard);
    return response;
  }

  [HttpPut("/{id}")]
  public TimecardResponse UpdateTimecard(string id, TimecardRequest updatedTimecard)
  {
    throw new NotImplementedException();
  }

  [HttpDelete("/{id}")]
  public BaseResponse RemoveTimecard(int id)
  {
    throw new NotImplementedException();
  }
}
