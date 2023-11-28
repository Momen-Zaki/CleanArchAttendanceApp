using CleanArchAttendanceApp.Core.Models;

namespace CleanArchAttendanceApp.WebApi.Endpoints.UserEndpoint;

public class GetAllResponse
{
    public IEnumerable<UserWithoutAttendanceDto>? Users { get; set; }
}
