using FastEndpoints;

namespace CleanArchAttendanceApp.WebApi.Endpoints.UserEndpoint;

public class ClockInRequest
{
    [FromClaim] public string? Id { get; set; }
}
