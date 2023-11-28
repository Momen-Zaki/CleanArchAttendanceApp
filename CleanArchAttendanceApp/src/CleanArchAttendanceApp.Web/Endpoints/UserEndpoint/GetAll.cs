using CleanArchAttendanceApp.Core.Interfaces;
using CleanArchAttendanceApp.Core.Models;
using FastEndpoints;

namespace CleanArchAttendanceApp.Web.Endpoints.UserEndpoint;

//public class GetAll : EndpointWithoutRequest<GetAllResponse, GetAllMapper>
public class GetAll : EndpointWithoutRequest<GetAllResponse>
{
  private readonly IAttendanceRepository _repository;

  public GetAll(IAttendanceRepository repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Get("users");
    Roles("Admin");
    Summary(s =>
    {
      s.Summary = "Get All Users";
      s.Description = "Get a list of all users";
      s.ResponseExamples[200] = new GetAllResponse
      {
        Users = new List<UserWithoutAttendanceDto>()
                { new UserWithoutAttendanceDto()
                          { Id = Guid.NewGuid(),
                            FullName = string.Empty,
                            UserName = string.Empty,
                            Role = UserRole.Employee
                      },
                        new UserWithoutAttendanceDto()
                          { Id = Guid.NewGuid(),
                            FullName = string.Empty,
                            UserName = string.Empty,
                            Role = UserRole.Employee
                      },
            }
      };
      s.Responses[200] = "ok with a list of all useres";
      s.Responses[404] = "Can't delete it for now";
    });
  }

  public override async Task HandleAsync(CancellationToken ct)
  {
    var entities = await _repository.GetAllUsersAsync();
    var users = new List<UserWithoutAttendanceDto>();
    foreach (var e in entities)
    {
      users.Add(new UserWithoutAttendanceDto()
      {
        Id = e.Id,
        FullName = e.FullName,
        UserName = e.UserName,
        Role = e.Role,
        //PasswrodHash = e.PasswrodHash,
      });
    }

    Response.Users = users;
    await SendAsync(Response);
    //Response.Users = Map.FromEntityAsync(entities, ct);
    //await SendMappedAsync(Response);
  }
}
