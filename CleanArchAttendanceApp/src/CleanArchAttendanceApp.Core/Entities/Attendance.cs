using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchAttendanceApp.Core.Entities;

public class Attendance
{
  [Key]
  public Guid Id { get; set; } = Guid.NewGuid();
  public DateTime AttendanceDay { get; set; } = DateTime.Now;
  public bool ClockedIn { get; set; } = false;
  public DateTime ClockedInAt { get; set; }
  public bool ClockedOut { get; set; } = false;
  public DateTime ClockedOutAt { get; set; }

  [ForeignKey("UserId")]
  public Guid UserId { get; set; }
  public User? User { get; set; }
}
