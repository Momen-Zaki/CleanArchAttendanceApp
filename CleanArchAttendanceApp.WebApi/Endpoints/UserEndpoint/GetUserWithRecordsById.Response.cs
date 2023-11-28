using CleanArchAttendanceApp.Core.Models;

namespace CleanArchAttendanceApp.WebApi.Endpoints.UserEndpoint;

public class GetUserWithRecordsByIdResponse
{
    public UserDto User { get; set; } = new UserDto();
}
