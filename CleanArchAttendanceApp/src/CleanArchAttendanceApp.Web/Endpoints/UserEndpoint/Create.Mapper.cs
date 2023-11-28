using CleanArchAttendanceApp.Core.Entities;
using CleanArchAttendanceApp.Core.Models;
using FastEndpoints;

namespace CleanArchAttendanceApp.Web.Endpoints.UserEndpoint;

public class CreateMapper : ResponseMapper<CreateResponse, User>
{
  public override CreateResponse FromEntity(User e) => new()
  {
    User = new UserWithoutAttendanceDto()
    {
      Id = e.Id,
      FullName = e.FullName,
      UserName = e.UserName,
      //PasswrodHash = e.PasswrodHash,
      Role = e.Role,
    }
  };
}
