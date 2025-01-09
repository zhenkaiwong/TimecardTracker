using System.ComponentModel.DataAnnotations;

namespace TimecardTracker.WebAPi.Models.Timecard;

public class TimecardRequest
{
  private const string timeRegex = @"^\d{2}:\d{2}$";
  private const string timeValidationError = "Time must be in HH:MM format.";

  public string Type { get; set; } = string.Empty;
  public string Title { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
  [RegularExpression(timeRegex, ErrorMessage = timeValidationError)]
  public string StartTime { get; set; } = string.Empty;
  [RegularExpression(timeRegex, ErrorMessage = timeValidationError)]
  public string EndTime { get; set; } = string.Empty;
  [RegularExpression(@"^\d{2}/\d{2}/\d{4}$", ErrorMessage = "Date must be in DD/MM/YYYY format.")]
  public string Created { get; set; } = string.Empty;
}
