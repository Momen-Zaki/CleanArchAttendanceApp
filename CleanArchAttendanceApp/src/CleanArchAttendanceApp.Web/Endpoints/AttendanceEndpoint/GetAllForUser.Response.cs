using CleanArchAttendanceApp.Core.Models;

namespace CleanArchAttendanceApp.Web.Endpoints.AttendanceEndpoint;

public class GetAllForUserResponse
{
  public List<AttendanceDto> AttendanceList { get; set; }
          = new List<AttendanceDto>();
}
