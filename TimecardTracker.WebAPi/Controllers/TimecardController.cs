using System.Net;
using Microsoft.AspNetCore.Mvc;
using TimecardTracker.WebAPi.Exceptions;
using TimecardTracker.WebAPi.Mappers;
using TimecardTracker.WebAPi.Models;
using TimecardTracker.WebAPi.Models.Timecard;
using TimecardTracker.WebAPi.Services;

namespace TimecardTracker.WebAPi.Controllers;

[ApiController]
[Route("timecard")]
public class TimecardController : ControllerBase
{
  private readonly ITimecardMapper _timecardMapper;
  private readonly ITimecardDataService _timecardDataService;
  public TimecardController(ITimecardMapper timecardMapper, ITimecardDataService timecardDataService)
  {
    _timecardMapper = timecardMapper;
    _timecardDataService = timecardDataService;
  }
  [HttpGet("id")]
  public async Task<IActionResult> GetSingleTimecardById(int id)
  {
    Timecard? timecard = await _timecardDataService.GetSingleTimecardByIdAsync(id);

    if (timecard is null)
    {
      return NotFound($"Unable to locate timecard with id \"{id}\"");
    }

    TimecardResponse response = _timecardMapper.MapToResponse(timecard);

    return Ok(response);
  }

  [HttpGet]
  public async Task<IActionResult> GetTimecards([FromQuery] Pagination pagination)
  {
    IEnumerable<Timecard> timecards = await _timecardDataService.GetTimecardsAsync(pagination);
    IEnumerable<TimecardResponse> response = _timecardMapper.MapEnumerableToResponses(timecards);
    return Ok(response);
  }

  [HttpPost]
  public async Task<IActionResult> InsertTimecard(TimecardRequest request)
  {
    Timecard timecardToCreate = _timecardMapper.MapFromRequest(request);
    Timecard newTimecard = await _timecardDataService.InsertTimecardAsync(timecardToCreate);
    TimecardResponse response = _timecardMapper.MapToResponse(newTimecard);

    return Ok(response);
  }

  [HttpPut("id")]
  public async Task<IActionResult> UpdateTimecard(int id, TimecardRequest request)
  {
    Timecard? currentTimecard = await _timecardDataService.GetSingleTimecardByIdAsync(id);

    if (currentTimecard is null)
    {
      return NotFound($"Unable to find timecard using ID \"{id}\"");
    }

    Timecard timecardFromRequest = _timecardMapper.MapFromRequest(request);

    Timecard latestTimecard = new Timecard(
      id,
      timecardFromRequest.Type,
      timecardFromRequest.Title,
      timecardFromRequest.Description,
      timecardFromRequest.StartTime,
      timecardFromRequest.EndTime,
      timecardFromRequest.Created
    );

    Timecard updatedTimecard = await _timecardDataService.UpdateTimecardAsync(currentTimecard, latestTimecard);
    TimecardResponse response = _timecardMapper.MapToResponse(updatedTimecard);
    return Ok(response);

  }

  [HttpDelete("id")]
  public async Task<IActionResult> RemoveTimecardById(int id)
  {
    try
    {
      await _timecardDataService.RemoveTimecardByIdAsync(id);
    }
    catch (NotFoundException ex)
    {
      return NotFound(ex.Message);
    }

    return Ok();
  }
}
