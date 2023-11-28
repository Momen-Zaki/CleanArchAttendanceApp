using CleanArchAttendanceApp.Core.Entities;
using CleanArchAttendanceApp.Core.Models;
using FastEndpoints;

namespace CleanArchAttendanceApp.Web.Endpoints.UserEndpoint;

public class UpdateMapper : ResponseMapper<UpdateResponse, User>
{
  public override UpdateResponse FromEntity(User e) => new()
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
