namespace TimeTracker.WebAPi.Models;

public class Timecard
{
  public int? Id { get; set; }
  public TimecardType Type { get; set; }
  public string Subject { get; set; } = string.Empty;
  public string Comment { get; set; } = string.Empty;
  public string StartTime { get; set; } = string.Empty;
  public string EndTime { get; set; } = string.Empty;
  public DateTime LastUpdated { get; set; }
  public DateOnly Created { get; set; }

}
