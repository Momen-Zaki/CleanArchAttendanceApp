using CleanArchAttendanceApp.Core.Interfaces;
using FastEndpoints;

namespace CleanArchAttendanceApp.Web.Endpoints.UserEndpoint;

public class ClockOut : Endpoint<ClockOutRequest, ClockOutResponse>
{
  private readonly IAttendanceRepository _repository;

  public ClockOut(IAttendanceRepository repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Get("users/{Id:Guid}/clockout");
    Roles();
    Description(x => x.WithName("ClockOut"));
    Summary(s =>
    {
      s.Summary = "Clock-Out for today attendance";
      s.Description = "employee can clock-out for today attendance";
      s.ExampleRequest = new ClockInRequest();
      s.ResponseExamples[200] = new ClockInResponse()
      { Messege = string.Empty };
      s.Responses[200] = "ok, with a messege to see u tomorrow .";
      s.Responses[403] = "forbidden";
    });
  }

  public override async Task HandleAsync(ClockOutRequest req, CancellationToken ct)
  {
    var routeId = Route<Guid>("Id");

    if (routeId.ToString() != req.Id)
      ThrowError("Unauthorized");

    var user = await _repository.GetUserByIdAsync(routeId);
    if (user == null)
      ThrowError("user not found");

    var attendanceForToday =
        await _repository.GetAttendanceRecordForTodayByUserIDAsync(routeId);

    if (attendanceForToday == null
        || attendanceForToday != null && attendanceForToday.ClockedIn == false)
    {
      ThrowError("pleas clock-in first");
    }
    else
    {
      attendanceForToday!.ClockedOut = true;
      attendanceForToday.ClockedOutAt = DateTime.Now;
      await _repository.SaveChangesAsync();
    }


    Response.Messege = "See u tomorrow";
    await SendAsync(Response, cancellation: ct);
  }
}
