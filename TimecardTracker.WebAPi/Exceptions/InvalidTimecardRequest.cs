using System;

namespace TimecardTracker.WebAPi.Exceptions;

public class InvalidTimecardRequest : Exception
{
  public InvalidTimecardRequest(string message) : base(message) { }
}
