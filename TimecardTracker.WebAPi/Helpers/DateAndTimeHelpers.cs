namespace TimecardTracker.WebAPi.Helpers;

public static class DateAndTimeHelpers
{
  public static TimeOnly MapFromTimeString(string timeString)
  {
    string[] timeStringInArray = timeString.Split(':');
    if (timeStringInArray.Length != 2)
    {
      throw new InvalidDataException($"Invalid time value or format. The correct time value should be in following format: HH:MM");
    }

    bool parseHourSuccess = int.TryParse(timeStringInArray[0], out int hour);
    bool parseMinuteSuccess = int.TryParse(timeStringInArray[1], out int minute);

    if (!parseHourSuccess || !parseMinuteSuccess)
    {
      throw new InvalidDataException($"Invalid time value is found. The value found is: ${timeString}");
    }

    if (hour > 23 || hour < 0)
    {
      throw new InvalidDataException($"Invalid hour: {hour}. Hour should between 0 to 23");
    }

    if (minute > 59 || minute < 0)
    {
      throw new InvalidDataException($"Invalid minute: {minute}. Minute should between 0 to 59");
    }

    return new TimeOnly(hour, minute);
  }

  public static DateOnly MapFromDateString(string dateString)
  {

    string[] requestDateInArray = dateString.Split('/');

    if (requestDateInArray.Length != 3)
    {
      throw new InvalidDataException("Invalid date value or format found. The correct date value should be in following format: DD/MM/YYYY. Example: 24/01/2025 indicates 24 January 2025");
    }

    bool parseDaySuccess = int.TryParse(requestDateInArray[0], out int day);
    bool parseMonthSuccess = int.TryParse(requestDateInArray[1], out int month);
    bool parseYearSuccess = int.TryParse(requestDateInArray[2], out int year);

    if (!parseDaySuccess || !parseMonthSuccess || !parseYearSuccess)
    {
      throw new InvalidDataException($"Invalid date value is found. Value: {dateString}");
    }

    if (year > 9999 || year < 1)
    {
      throw new InvalidDataException($"Invalid year: {year}. Year value should between 1 to 9999");
    }

    if (month > 12 || month < 0)
    {
      throw new InvalidDataException($"Invalid month: {month}. Month value should between 1 to 12");
    }

    int daysInMonth = DateTime.DaysInMonth(year, month);

    if (day > daysInMonth || day < 1)
    {
      throw new InvalidDataException($"Invalid day: {day}.Day value should between 1 to {daysInMonth}");
    }

    return new DateOnly(year, month, day);
  }

}
