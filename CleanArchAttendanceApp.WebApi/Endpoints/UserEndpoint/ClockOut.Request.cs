using FastEndpoints;

namespace CleanArchAttendanceApp.WebApi.Endpoints.UserEndpoint;

public class ClockOutRequest
{
    [FromClaim] public string? Id { get; set; }
}
