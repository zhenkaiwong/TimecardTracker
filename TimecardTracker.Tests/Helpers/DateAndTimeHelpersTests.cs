using TimecardTracker.WebAPi.Helpers;

namespace TimecardTracker.Tests.Helpers;

public class DateAndTimeHelpersTests
{
  public static IEnumerable<object[]> _validTimeDataSet => new List<object[]>() {
   new object[] {"13:00", new TimeOnly(13, 0)},
   new object[] {"21:00", new TimeOnly(21, 0)},
   new object[] {"23:51", new TimeOnly(23, 51)},
  };

  [Theory]
  [MemberData(nameof(_validTimeDataSet))]
  public void MapFromTimeString_ValidTimeString_ReturnResult(string testValue, TimeOnly expectedValue)
  {
    TimeOnly result = DateAndTimeHelpers.MapFromTimeString(testValue);
    Assert.Equal(expectedValue, result);
  }
  [Theory]
  [InlineData("25:00")]
  [InlineData("23:61")]
  [InlineData("AA:00")]
  [InlineData("13:BB")]
  [InlineData("abcd")]
  [InlineData("19:19:19")]
  [InlineData("")]
  [InlineData("::")]
  [InlineData(" : : ")]
  public void MapFromTimeString_InvalidTimeString_ThrowsError(string testValue)
  {

    Assert.Throws<InvalidDataException>(() =>
    {
      TimeOnly result = DateAndTimeHelpers.MapFromTimeString(testValue);
    });
  }

  public static IEnumerable<object[]> _validDateDataSet => new List<object[]>() {
    new object[] {"12/12/2024", new DateOnly(2024, 12, 12)},
    new object[] {"1/9/2025", new DateOnly(2025, 9, 1)},
    new object[] {"29/2/2024", new DateOnly(2024, 2, 29)},
  };

  [Theory]
  [MemberData(nameof(_validDateDataSet))]
  public void MapFromDateString_ValidDateString_ReturnsResult(string testValue, DateOnly expectedValue)
  {
    DateOnly result = DateAndTimeHelpers.MapFromDateString(testValue);
    Assert.Equal(expectedValue, result);
  }

  [Theory]
  [InlineData("32/12/2025")]
  [InlineData("1/13/2025")]
  [InlineData(" / / ")]
  [InlineData("11")]
  [InlineData("11/11")]
  [InlineData("DD/MM/YYYY")]
  [InlineData("29/2/2025")]
  public void MapFromDateString_InvalidDateString_ThrowsError(string testValue)
  {
    Assert.Throws<InvalidDataException>(() =>
    {
      DateOnly result = DateAndTimeHelpers.MapFromDateString(testValue);
    });
  }
}
