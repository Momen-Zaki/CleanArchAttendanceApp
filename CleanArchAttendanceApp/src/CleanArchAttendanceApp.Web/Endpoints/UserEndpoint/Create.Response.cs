using CleanArchAttendanceApp.Core.Models;

namespace CleanArchAttendanceApp.Web.Endpoints.UserEndpoint;

public class CreateResponse
{
  public UserWithoutAttendanceDto? User { get; set; }
}
