using CleanArchAttendanceApp.Core.Interfaces;
using CleanArchAttendanceApp.Core.Models;
using FastEndpoints;

namespace CleanArchAttendanceApp.WebApi.Endpoints.AttendanceEndpoint;

public class CreateForUser
      : Endpoint<CreateForUserRequest, CreateForUserResponse>
{
    private readonly IAttendanceRepository _repository;

    public CreateForUser(IAttendanceRepository repository)
    {
        _repository = repository;
    }

    public override void Configure()
    {
        Post("users/{Id:Guid}/attendance");
        Roles("admin");
        Summary(s =>
        {
            s.Summary = "Create a new Attendance Record for a User";
            s.Description = "Create a new Attendance Record for a User by his Id";
            s.ExampleRequest = new CreateForUserRequest { };
            s.ResponseExamples[200] = new CreateForUserResponse()
            { AttendaceRecordCreated = new AttendanceDto() { } };
            s.Responses[200] = "return the created Attendance Record";
        });
    }

    public override async Task HandleAsync(CreateForUserRequest req, CancellationToken ct)
    {
        var userId = Route<Guid>("Id");

        if (!await _repository.UserExistsWithIdAsync(userId))
            ThrowError("user not found");

        var newAttendanceDto = new AttendanceDto
        {
            AttendanceDay = req.AttendanceDay,
            ClockedInAt = req.ClockedInAt,
            ClockedOutAt = req.ClockedOutAt,
            ClockedIn = req.ClockedIn,
            ClockedOut = req.ClockedOut,
        };

        var newAttendance = await _repository
            .AddAttendanceRecordForUser(newAttendanceDto, userId);

        var createdAttendance = new AttendanceDto() 
        { 
            Id = newAttendance.Id,
            AttendanceDay = newAttendance.AttendanceDay,
            ClockedInAt = newAttendance.ClockedInAt,
            ClockedOut = newAttendance.ClockedOut,
            ClockedIn = newAttendance.ClockedIn,
            ClockedOutAt = newAttendance.ClockedOutAt,
            UserId = newAttendance.UserId,
        };
        Response.AttendaceRecordCreated = createdAttendance;
        await Task.FromResult(Response);
    }
}
