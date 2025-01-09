namespace TimecardTracker.WebAPi.Models.Timecard;

public class Timecard
{
  public int Id { get; protected set; }
  public TimecardType Type { get; protected set; }
  public string Title { get; protected set; }
  public string Description { get; protected set; } = string.Empty;
  public TimeOnly StartTime { get; protected set; }
  public TimeOnly EndTime { get; protected set; }
  public DateOnly Created { get; protected set; }

  public Timecard(int id, TimecardType type, string title, string description, TimeOnly startTime, TimeOnly endTime, DateOnly created) : this(type, title, description, startTime, endTime, created)
  {
    Id = id;
  }
  public Timecard(TimecardType type, string title, string description, TimeOnly startTime, TimeOnly endTime, DateOnly created)
  {
    Type = type;
    Title = title;
    Description = description;
    StartTime = startTime;
    EndTime = endTime;
    Created = created;
  }

  public bool Equals(Timecard timecard)
  {
    bool idMatch = Id.Equals(timecard.Id);
    bool typeMatch = Type.Equals(timecard.Type);
    bool titleMatch = Title.Equals(timecard.Title);
    bool descriptionMatch = Description.Equals(timecard.Description);
    bool startTimeMatch = StartTime.Equals(timecard.StartTime);
    bool endTimeMatch = EndTime.Equals(timecard.EndTime);
    bool createdMatch = Created.Equals(timecard.Created);

    return idMatch && typeMatch && titleMatch && descriptionMatch && startTimeMatch && endTimeMatch && createdMatch;
  }

  public void UpdateValue(Timecard newTimecard)
  {
    Type = newTimecard.Type;
    Title = newTimecard.Title;
    Description = newTimecard.Description;
    StartTime = newTimecard.StartTime;
    EndTime = newTimecard.EndTime;
    Created = newTimecard.Created;
  }
}
