using FastEndpoints;

namespace CleanArchAttendanceApp.Web.Endpoints.UserEndpoint;

public class ClockOutRequest
{
  [FromClaim] public string? Id { get; set; }
}
