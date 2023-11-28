using CleanArchAttendanceApp.Core.Models;

namespace CleanArchAttendanceApp.Web.Endpoints.UserEndpoint;

public class GetAllResponse
{
  public IEnumerable<UserWithoutAttendanceDto>? Users { get; set; }
}
