using System;
using System.ComponentModel.DataAnnotations;

namespace TimeTracker.WebAPi.Models;

public class Pagination
{
  public int Skip { get; set; }
  [Range(1, 100)]
  public int Rows { get; set; }
  [MaxLength(250)]
  public string Search { get; set; } = string.Empty;
}
