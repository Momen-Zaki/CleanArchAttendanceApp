using CleanArchAttendanceApp.Core.Entities;
using CleanArchAttendanceApp.Core.Interfaces;
using CleanArchAttendanceApp.Core.Models;
using CleanArchAttendanceApp.Infrastructure.DbContexts;
using FastEndpoints;

namespace CleanArchAttendanceApp.Web.Endpoints.UserEndpoint;

public class Create : Endpoint<CreateRequest, CreateResponse, CreateMapper>
{
  private readonly AttendanceContext _context;
  private readonly IAttendanceRepository _reposityory;

  public Create(AttendanceContext context, IAttendanceRepository reposityory)
  {
    _context = context;
    _reposityory = reposityory;
  }

  public override void Configure()
  {
    Post("/users/create");
    Roles("Admin");
    Summary(s =>
    {
      s.Summary = "Create a new user";
      s.Description = "creates a new user";
      s.ExampleRequest = new CreateRequest()
      {
        FullName = string.Empty,
        UserName = string.Empty,
        Password = string.Empty,
        Role = UserRole.Employee
      };
      s.ResponseExamples[200] = new CreateResponse()
      {
        User = new UserWithoutAttendanceDto()
        {
          Id = Guid.NewGuid(),
          FullName = string.Empty,
          UserName = string.Empty,
          Role = UserRole.Employee
        }
      };
      s.Responses[200] = "returns the created user";
      s.Responses[401] = "Unauthorized";
      s.Responses[403] = "forbidden";
    });
  }

  public override async Task HandleAsync(CreateRequest req, CancellationToken ct)
  {

    var hashedPassword = BCrypt.Net.BCrypt.HashPassword(req.Password);

    // NEED More Work
    if (req.Role != UserRole.Employee && req.Role != UserRole.Admin)
      AddError(r => r.Role!, "Role is not valid");
    //ThrowError("Invalid User Role");

    if (await _reposityory.GetUserByUserNameAsync(req.UserName!) != null)
      AddError(r => r.UserName!, "Username is taken");

    ThrowIfAnyErrors();

    var newUser = new User()
    {
      FullName = req.FullName,
      UserName = req.UserName,
      PasswrodHash = hashedPassword,
      Role = req.Role,
    };
    _context.Users.Add(newUser);
    await _reposityory.SaveChangesAsync();

    var userCreated = _context.Users
        .FirstOrDefault(u => u.UserName == newUser.UserName);
    if (userCreated == null)
      ThrowError("can't create a new user for now");

    Response = Map.FromEntity(userCreated!);
    await SendCreatedAtAsync<GetById>("GetUserById", Response);
  }
}
