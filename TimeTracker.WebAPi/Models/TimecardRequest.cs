using System.Text.Json.Serialization;
using TimeTracker.WebAPi.Services.Converter;

namespace TimeTracker.WebAPi.Models;
public class TimecardRequest
{
  public string? Id { get; set; }
  public string? Type { get; set; }
  public string? Subject { get; set; }
  public string? Comment { get; set; }
  [JsonConverter(typeof(DateConverter))]
  public DateOnly Created { get; set; }
  public string StartTime { get; set; } = string.Empty;
  public string EndTime { get; set; } = string.Empty;
}
