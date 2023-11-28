using System.Linq;
using CleanArchAttendanceApp.Core.Entities;
using CleanArchAttendanceApp.Core.Models;
using FastEndpoints;

namespace CleanArchAttendanceApp.Web.Endpoints.UserEndpoint;

public class GetAllMapper : ResponseMapper<GetAllResponse, IEnumerable<User>>
{
  public override Task<GetAllResponse> FromEntityAsync(
      IEnumerable<User> entities, CancellationToken ct = default)
  {
    var response = new GetAllResponse();
    foreach (var e in entities)
    {
      response.Users!.Append(new UserWithoutAttendanceDto()
      {
        Id = e.Id,
        FullName = e.FullName,
        UserName = e.UserName,
        //PasswrodHash = e.PasswrodHash,
      });
    }
    return Task.FromResult(response);
  }
}
