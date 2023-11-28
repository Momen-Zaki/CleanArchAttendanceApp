namespace CleanArchAttendanceApp.Web.Endpoints.AttendanceEndpoint;

public class CreateForUserRequest
{
  public DateTime AttendanceDay { get; set; } = DateTime.Now;
  public bool ClockedIn { get; set; } = false;
  public DateTime ClockedInAt { get; set; } = new DateTime();
  public bool ClockedOut { get; set; } = false;
  public DateTime ClockedOutAt { get; set; } = new DateTime();
}
