using CleanArchAttendanceApp.Core.Entities;
using CleanArchAttendanceApp.Core.Models;
using FastEndpoints;

namespace CleanArchAttendanceApp.WebApi.Endpoints.UserEndpoint;

public class GetUserWithRecordsByIdMapper
      : ResponseMapper<GetUserWithRecordsByIdResponse, User>
{
    public override GetUserWithRecordsByIdResponse FromEntity(User e) => new()
    {
        User = new UserDto()
        {
            Id = e.Id,
            FullName = e.FullName,
            UserName = e.UserName,
            //PasswrodHash = e.PasswrodHash,
            Role = e.Role,
            AttendanceRecords = e.AttendanceRecords
        }
    };
}
