using CleanArchAttendanceApp.Core.Models;

namespace CleanArchAttendanceApp.WebApi.Endpoints.UserEndpoint;

public class UpdateResponse
{
    public UserWithoutAttendanceDto User { get; set; } = new UserWithoutAttendanceDto();
}
