using TimecardTracker.WebAPi.Exceptions;
using TimecardTracker.WebAPi.Helpers;
using TimecardTracker.WebAPi.Models.Timecard;

namespace TimecardTracker.Tests.Helpers;

public class TimecardTypeHelpersTests
{
  public static IEnumerable<object[]> _validTimecardTypeTestData = new List<object[]>(){
    new object[] { "Activity", TimecardType.Activity},
    new object[] { "Ticket", TimecardType.Ticket},
  };

  [Theory]
  [MemberData(nameof(_validTimecardTypeTestData))]
  public void MapFromStringToTimecardType_ValidTypeString_ReturnsResult(string testValue, TimecardType expectedValue)
  {
    TimecardType result = TimecardTypeHelpers.MapFromStringToTimecardType(testValue);

    Assert.Equal(expectedValue, result);
  }

  [Theory]
  [InlineData("Invalid Type")]
  [InlineData("activity")]
  [InlineData("ticket")]
  public void MapFromStringToTimecardType_InvalidString_ThrowsResult(string testValue)
  {
    Assert.Throws<InvalidTimecardTypeException>(() =>
    {
      TimecardType result = TimecardTypeHelpers.MapFromStringToTimecardType(testValue);
    });
  }
}
