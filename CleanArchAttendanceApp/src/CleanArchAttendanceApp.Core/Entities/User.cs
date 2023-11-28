using System.ComponentModel.DataAnnotations;

namespace CleanArchAttendanceApp.Core.Entities;

public class User
{
  [Key]
  public Guid Id { get; set; } = Guid.NewGuid();
  public string? FullName { get; set; }
  public string? UserName { get; set; }
  public string? PasswrodHash { get; set; }
  public string? Role { get; set; }
  public ICollection<Attendance> AttendanceRecords { get; set; }
      = new List<Attendance>();
}
