namespace TimeTracker.WebAPi.Models;

public class TimecardResponse : BaseResponse
{
  public int? Id { get; set; }
  public string? Type { get; set; }
  public string? Subject { get; set; }
  public string? Comment { get; set; }
  public string StartTime { get; set; } = string.Empty;
  public string EndTime { get; set; } = string.Empty;
  public DateTime LastUpdated { get; set; }
  public DateOnly Created { get; set; }
}
