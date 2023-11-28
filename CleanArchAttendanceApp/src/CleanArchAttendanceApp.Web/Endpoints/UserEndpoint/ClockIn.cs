using CleanArchAttendanceApp.Core.Entities;
using CleanArchAttendanceApp.Core.Interfaces;
using FastEndpoints;

namespace CleanArchAttendanceApp.Web.Endpoints.UserEndpoint;

public class ClockIn : Endpoint<ClockInRequest, ClockInResponse>
{
  private readonly IAttendanceRepository _repository;

  public ClockIn(IAttendanceRepository repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Get("users/{Id:Guid}/clockin");
    Roles();
    Description(x => x.WithName("ClockIn"));
    Summary(s =>
    {
      s.Summary = "Clock-in for today attendance";
      s.Description = "employee can clock-in for today attendance";
      s.ExampleRequest = new ClockInRequest();
      s.ResponseExamples[200] = new ClockInResponse()
      { Messege = string.Empty };
      s.Responses[200] = "ok, with a messege to wish u a good day.";
      s.Responses[403] = "forbidden";
    });
  }

  public override async Task HandleAsync(ClockInRequest req, CancellationToken ct)
  {
    var routeId = Route<Guid>("Id");

    if (routeId.ToString() != req.Id)
      ThrowError("Unauthorized");

    var user = await _repository.GetUserByIdAsync(routeId);
    if (user == null)
      ThrowError("user not found");

    var attendanceForToday =
        await _repository.GetAttendanceRecordForTodayByUserIDAsync(routeId);
    if (attendanceForToday == null)
    {
      var newAttendance = new Attendance()
      {
        AttendanceDay = DateTime.Now,
        ClockedIn = true,
        ClockedInAt = DateTime.Now,
        ClockedOut = false,
        ClockedOutAt = new DateTime()
      };
      await _repository.AddAttendanceRecord(newAttendance, routeId);
    }
    else
    {
      attendanceForToday.ClockedIn = true;
      attendanceForToday.ClockedInAt = DateTime.Now;
    }


    Response.Messege = "have a nice day";
    await SendAsync(Response, cancellation: ct);
  }
}
