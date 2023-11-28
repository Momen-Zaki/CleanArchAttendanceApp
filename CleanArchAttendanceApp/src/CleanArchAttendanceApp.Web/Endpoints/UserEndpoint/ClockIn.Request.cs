using FastEndpoints;

namespace CleanArchAttendanceApp.Web.Endpoints.UserEndpoint;

public class ClockInRequest
{
  [FromClaim] public string? Id { get; set; }
}
