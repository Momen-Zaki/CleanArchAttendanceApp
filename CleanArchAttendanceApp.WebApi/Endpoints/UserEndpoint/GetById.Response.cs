using CleanArchAttendanceApp.Core.Models;

namespace CleanArchAttendanceApp.WebApi.Endpoints.UserEndpoint;

public class GetByIdResponse
{
    public UserWithoutAttendanceDto User { get; set; } = new UserWithoutAttendanceDto();
}
