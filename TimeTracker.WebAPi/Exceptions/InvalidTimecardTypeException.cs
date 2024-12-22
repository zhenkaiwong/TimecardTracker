namespace TimeTracker.WebAPi.Exceptions;

public class InvalidTimecardTypeException : Exception
{
  public InvalidTimecardTypeException() { }
  public InvalidTimecardTypeException(string message) : base(message) { }
}
