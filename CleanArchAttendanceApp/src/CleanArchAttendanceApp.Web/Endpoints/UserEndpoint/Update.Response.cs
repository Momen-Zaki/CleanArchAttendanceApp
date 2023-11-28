using CleanArchAttendanceApp.Core.Models;

namespace CleanArchAttendanceApp.Web.Endpoints.UserEndpoint;

public class UpdateResponse
{
  public UserWithoutAttendanceDto User { get; set; } = new UserWithoutAttendanceDto();
}
