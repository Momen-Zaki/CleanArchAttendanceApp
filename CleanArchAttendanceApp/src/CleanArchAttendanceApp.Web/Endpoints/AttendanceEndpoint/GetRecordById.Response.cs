using CleanArchAttendanceApp.Core.Models;

namespace CleanArchAttendanceApp.Web.Endpoints.AttendanceEndpoint;

public class GetRecordByIdResponse
{
  public AttendanceDto? Record { get; set; }
}
