using TimecardTracker.WebAPi.Exceptions;
using TimecardTracker.WebAPi.Helpers;
using TimecardTracker.WebAPi.Models.Timecard;

namespace TimecardTracker.WebAPi.Mappers;

public class TimecardMapper : ITimecardMapper
{
  private class FieldToValidate
  {
    public string Name { get; } = string.Empty;
    public string Value { get; } = string.Empty;

    public FieldToValidate(string fieldName, string fieldValue)
    {
      Name = fieldName;
      Value = fieldValue;
    }
  }

  private bool ValidateTimecardAndGetErrorMessage(TimecardRequest request, out string errorMessage)
  {
    bool validateStringIsEmptyOrNull(FieldToValidate field, out string errorMessage)
    {
      if (string.IsNullOrEmpty(field.Value))
      {
        errorMessage = $"Unable to locate value from field \"{field.Name}\". The value is either null or empty";
        return false;
      }

      errorMessage = string.Empty;
      return true;
    }

    bool validateTimeFormatAndValue(FieldToValidate field, out string errorMessage)
    {
      try
      {
        TimeOnly value = DateAndTimeHelpers.MapFromTimeString(field.Value);
      }
      catch (InvalidDataException ex)
      {
        errorMessage = $"{ex.Message}. Field: {field.Name}";
        return false;
      }

      errorMessage = string.Empty;
      return true;
    }

    Queue<FieldToValidate> fieldsToValidateStringNullOrEmpty = new();

    FieldToValidate title = new("title", request.Title);
    FieldToValidate type = new("type", request.Type);
    FieldToValidate created = new("created", request.Created);
    FieldToValidate startTime = new("startTime", request.StartTime);
    FieldToValidate endTime = new("endTime", request.EndTime);

    fieldsToValidateStringNullOrEmpty.Append(title);
    fieldsToValidateStringNullOrEmpty.Append(type);
    fieldsToValidateStringNullOrEmpty.Append(created);
    fieldsToValidateStringNullOrEmpty.Append(startTime);
    fieldsToValidateStringNullOrEmpty.Append(endTime);

    while (fieldsToValidateStringNullOrEmpty.Any())
    {
      FieldToValidate fieldToValidate = fieldsToValidateStringNullOrEmpty.Dequeue();

      if (!validateStringIsEmptyOrNull(fieldToValidate, out string fieldError))
      {
        errorMessage = fieldError;
        return false;
      }
    }

    try
    {
      DateOnly createdValue = DateAndTimeHelpers.MapFromDateString(created.Value);
    }
    catch (InvalidDataException ex)
    {
      errorMessage = ex.Message;
      return false;
    }

    if (!validateTimeFormatAndValue(startTime, out string startTimeError))
    {
      errorMessage = startTimeError;
      return false;
    }

    if (!validateTimeFormatAndValue(endTime, out string endTimeError))
    {
      errorMessage = endTimeError;
      return false;
    }

    errorMessage = string.Empty;
    return true;
  }
  public Timecard MapFromRequest(TimecardRequest request)
  {
    bool requestIsValid = ValidateTimecardAndGetErrorMessage(request, out string validationErrorMessage);

    if (!requestIsValid)
    {
      throw new InvalidTimecardRequest(validationErrorMessage);
    }

    TimecardType timecardType = TimecardTypeHelpers.MapFromStringToTimecardType(request.Type);
    TimeOnly startTime = DateAndTimeHelpers.MapFromTimeString(request.StartTime);
    TimeOnly endTime = DateAndTimeHelpers.MapFromTimeString(request.EndTime);
    DateOnly created = DateAndTimeHelpers.MapFromDateString(request.Created);

    return new Timecard(timecardType, request.Title, request.Description, startTime, endTime, created);
  }

  public TimecardResponse MapToResponse(Timecard timecard)
  {
    string type = timecard.Type.ToString();
    string startTime = $"{timecard.StartTime.Hour}:{string.Format("{0:00}", timecard.StartTime.Minute)}";
    string endTime = $"{timecard.EndTime.Hour}:{string.Format("{0:00}", timecard.EndTime.Minute)}";
    string createdDate = $"{timecard.Created.Day}/{timecard.Created.Month}/{timecard.Created.Year}";

    TimecardResponse response = new TimecardResponse(timecard.Id.ToString(), type, timecard.Title, timecard.Description, startTime, endTime, createdDate);

    return response;
  }

  public IEnumerable<TimecardResponse> MapEnumerableToResponses(IEnumerable<Timecard> timecards)
  {
    return timecards.Select(timecard => MapToResponse(timecard));
  }
}
