using CleanArchAttendanceApp.Core.Models;

namespace CleanArchAttendanceApp.Web.Endpoints.UserEndpoint;

public class GetByIdResponse
{
  public UserWithoutAttendanceDto User { get; set; } = new UserWithoutAttendanceDto();
}
