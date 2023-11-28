using CleanArchAttendanceApp.Core.Models;

namespace CleanArchAttendanceApp.Web.Endpoints.UserEndpoint;

public class GetUserWithRecordsByIdResponse
{
  public UserDto User { get; set; } = new UserDto();
}
