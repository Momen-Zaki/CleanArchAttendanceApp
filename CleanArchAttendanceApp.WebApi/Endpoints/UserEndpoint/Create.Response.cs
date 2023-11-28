using CleanArchAttendanceApp.Core.Models;

namespace CleanArchAttendanceApp.WebApi.Endpoints.UserEndpoint;

public class CreateResponse
{
    public UserWithoutAttendanceDto? User { get; set; }
}
