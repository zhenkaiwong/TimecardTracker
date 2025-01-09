namespace TimecardTracker.WebAPi.Exceptions;

public class InvalidTimecardTypeException : Exception
{
  public InvalidTimecardTypeException(string message) : base(message)
  {

  }
}
