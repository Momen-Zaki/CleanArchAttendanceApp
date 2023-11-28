using CleanArchAttendanceApp.Core.Entities;
using CleanArchAttendanceApp.Core.Models;
using FastEndpoints;

namespace CleanArchAttendanceApp.WebApi.Endpoints.UserEndpoint;

public class GetByIdMapper : ResponseMapper<GetByIdResponse, User>
{
    public override GetByIdResponse FromEntity(User e) => new()
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
