using CleanArchAttendanceApp.Core.Interfaces;
using CleanArchAttendanceApp.Core.Models;
using FastEndpoints;

namespace CleanArchAttendanceApp.Web.Endpoints.UserEndpoint;

public class Update : Endpoint<UpdateRequest, UpdateResponse, UpdateMapper>
{
  private readonly IAttendanceRepository _repository;

  public Update(IAttendanceRepository repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Put("users/{Id:Guid}");
    Roles("Admin");
    Summary(s =>
    {
      s.Summary = "Update a User by Id";
      s.Description = "updates a User with the give id " +
                "if it exists with the new user object passed";
      s.ExampleRequest = new UpdateRequest
      {
        FullName = string.Empty,
        UserName = string.Empty,
        Password = string.Empty,
        Role = UserRole.Employee
      };
      s.ResponseExamples[200] = new UpdateResponse
      {
        User = new UserWithoutAttendanceDto()
        {
          Id = Guid.NewGuid(),
          FullName = string.Empty,
          UserName = string.Empty,
          Role = UserRole.Employee
        }
      };
      s.Responses[200] = "ok with the modified user data";
      s.Responses[404] = "Can't find a user with this Id";
    });
  }
  public override async Task HandleAsync(UpdateRequest req, CancellationToken ct)
  {
    var userId = Route<Guid>("Id");
    var user = await _repository.GetUserByIdAsync(userId);
    if (user == null)
    {
      ThrowError("User not found");
    }
    user.FullName = req.FullName;
    user.UserName = req.UserName;
    user.Role = req.Role;
    user.PasswrodHash = BCrypt.Net.BCrypt.HashPassword(req.Password);
    if (!await _repository.SaveChangesAsync())
      ThrowError("Can't update user for now!");

    Response = Map.FromEntity(user);
    await SendAsync(Response, cancellation: ct);
  }
}
