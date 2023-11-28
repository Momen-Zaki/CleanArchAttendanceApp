using System;

namespace CleanArchAttendanceApp.Core.Models;

public class UserWithoutAttendanceDto
{
  public Guid Id { get; set; }
  public string? FullName { get; set; }
  public string? UserName { get; set; }
  //public string PasswrodHash { get; set; }
  public string? Role { get; set; }
}
