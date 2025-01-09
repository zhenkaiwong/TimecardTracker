using TimecardTracker.WebAPi.Exceptions;
using TimecardTracker.WebAPi.Mappers;
using TimecardTracker.WebAPi.Models.Timecard;

namespace TimecardTracker.Tests.Mappers;

public class TimecardMapperTests
{
  private readonly ITimecardMapper mapper = new TimecardMapper();
  public static IEnumerable<object[]> timecardRequestValidDataSet = new List<object[]>() {
    new object[] {
      new TimecardRequest() {
        Type = TimecardType.Activity.ToString(),
        Title = "Test time card",
        Description = "Test description",
        StartTime = "12:00",
        EndTime = "12:35",
        Created = "24/12/2024"
      },
      new Timecard(TimecardType.Activity, "Test time card", "Test description", new TimeOnly(12, 0), new TimeOnly(12, 35), new DateOnly(2024, 12, 24))
    },
  };

  [Theory]
  [MemberData(nameof(timecardRequestValidDataSet))]
  public void MapFromRequest_ValidRequestData_ReturnsResult(TimecardRequest testValue, Timecard expectedValue)
  {
    Timecard result = mapper.MapFromRequest(testValue);
    Assert.True(result.Equals(expectedValue));
  }

  public static IEnumerable<object[]> timecardRequestInvalidDataSet = new List<object[]>() {
    new object[] {
      new TimecardRequest() {
        Type = "Not exist",
        Title = "Test time card",
        Description = "Test description",
        StartTime = "25:00",
        EndTime = "12:330",
        Created = "220/13/22024"
      }
    },
  };

  [Theory]
  [MemberData(nameof(timecardRequestInvalidDataSet))]
  public void MapFromRequest_InvalidRequestData_ThrowsError(TimecardRequest testValue)
  {
    Assert.Throws<InvalidTimecardRequest>(() =>
    {
      Timecard result = mapper.MapFromRequest(testValue);
    });
  }


  public static IEnumerable<object[]> timecardValidDataSet = new List<object[]>() {
    new object[] {
      new Timecard(1, TimecardType.Activity, "Test time card", "Test description", new TimeOnly(12, 0), new TimeOnly(12, 35), new DateOnly(2024, 2, 3)),
      new TimecardResponse("1", TimecardType.Activity.ToString(), "Test time card", "Test description", "12:00", "12:35", "3/2/2024")
    },
  };

  [Theory]
  [MemberData(nameof(timecardValidDataSet))]
  public void MapToResponse_ValidTimecard_ReturnsResult(Timecard testValue, TimecardResponse expectedResult)
  {
    TimecardResponse response = mapper.MapToResponse(testValue);
    Assert.True(response.Equals(expectedResult));
  }
}
