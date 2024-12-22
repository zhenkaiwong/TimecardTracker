using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TimeTracker.WebAPi.Services.Converter;

public class DateConverter : JsonConverter<DateOnly>
{
  protected virtual Tuple<int, int, int> ConvertRawDateToDateArray(string rawDate)
  {
    string[] rawDateArray = rawDate.Split('/');
    if (rawDateArray.Length != 3)
    {
      throw new InvalidDataException($"\"{rawDateArray}\" is an incorrect date format. Date must be MM/DD/YYYY. Eg 24 Dec 2024 -> 12/24/2024");
    }

    int month = int.Parse(rawDateArray[0]);
    int day = int.Parse(rawDateArray[1]);
    int year = int.Parse(rawDateArray[2]);

    return new Tuple<int, int, int>(month, day, year);
  }
  public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    string value = reader.GetString() ?? string.Empty;
    if (string.IsNullOrEmpty(value))
    {
      throw new InvalidDataException("Field cannot be empty");
    }
    Tuple<int, int, int> dateTuple = ConvertRawDateToDateArray(value);
    int month = dateTuple.Item1;
    int day = dateTuple.Item2;
    int year = dateTuple.Item3;
    DateOnly date = new DateOnly(year, month, day);
    return date;
  }

  public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
  {
    throw new NotImplementedException();
  }
}
