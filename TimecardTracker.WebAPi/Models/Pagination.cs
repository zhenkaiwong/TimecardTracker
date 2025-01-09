namespace TimecardTracker.WebAPi.Models;

public class Pagination
{
  public int Take { get; set; }
  public int Skip { get; set; }
  public string? WeekStartDate { get; set; }
  public string? Type { get; set; }
  public string? Description { get; set; }
}
