namespace TimecardTracker.WebAPi.Models.Timecard;

public class TimecardResponse
{
  public string Id { get; set; } = string.Empty;
  public string Type { get; set; } = string.Empty;
  public string Title { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
  public string StartTime { get; set; } = string.Empty;
  public string EndTime { get; set; } = string.Empty;
  public string Created { get; set; } = string.Empty;

  public TimecardResponse(string id, string type, string title, string description, string startTime, string endTime, string created)
  {
    Id = id;
    Type = type;
    Title = title;
    Description = description;
    StartTime = startTime;
    EndTime = endTime;
    Created = created;
  }

  public bool Equals(TimecardResponse response)
  {

    bool idMatch = Id.Equals(response.Id);
    bool typeMatch = Type.Equals(response.Type);
    bool titleMatch = Title.Equals(response.Title);
    bool descriptionMatch = Description.Equals(response.Description);
    bool startTimeMatch = StartTime.Equals(response.StartTime);
    bool endTimeMatch = EndTime.Equals(response.EndTime);
    bool createdMatch = Created.Equals(response.Created);

    return idMatch && typeMatch && titleMatch && descriptionMatch && startTimeMatch && endTimeMatch && createdMatch;
  }
}
