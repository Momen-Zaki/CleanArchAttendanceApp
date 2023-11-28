using CleanArchAttendanceApp.Core.Interfaces;
using CleanArchAttendanceApp.Core.Models;
using FastEndpoints;

namespace CleanArchAttendanceApp.WebApi.Endpoints.AttendanceEndpoint;

public class CreateForUser
      : Endpoint<CreateForUserRequest, CreateForUserResponse, CreateForUserMapper>
{
    private readonly IAttendanceRepository _repository;

    public CreateForUser(IAttendanceRepository repository)
    {
        _repository = repository;
    }

    public override void Configure()
    {
        Post("users/{Id:Guid}/attendance");
        Roles("Admin");
        Summary(s =>
        {
            s.Summary = "Create a new Attendance Record for a User";
            s.Description = "Create a new Attendance Record for a User by his Id";
            s.ExampleRequest = new CreateForUserRequest { };
            s.ResponseExamples[200] = new CreateForUserResponse()
            { AttendaceRecordCreated = new AttendanceDto() { } };
            s.Responses[200] = "return the created Attendance Record";
            //s.Responses[401] = "Unauthorized";
            //s.Responses[403] = "Forbidden";
        });
    }

    public override async Task HandleAsync(CreateForUserRequest req, CancellationToken ct)
    {
        var userId = Route<Guid>("Id");

        if (!await _repository.UserExistsWithIdAsync(userId))
            ThrowError("user not found");

        var newAttendance = Map.ToEntity(req);
        newAttendance = await _repository
            .AddAttendanceRecord(newAttendance, userId);

        Response = Map.FromEntity(newAttendance);
        await Task.FromResult(Response);
    }
}
