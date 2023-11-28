using CleanArchAttendanceApp.Core.Entities;
using CleanArchAttendanceApp.Core.Models;
using FastEndpoints;

namespace CleanArchAttendanceApp.WebApi.Endpoints.AttendanceEndpoint;

public class CreateForUserMapper
      : Mapper<CreateForUserRequest, CreateForUserResponse, Attendance>
{
    public override Attendance ToEntity(CreateForUserRequest a) => new()
    {
        AttendanceDay = a.AttendanceDay,
        ClockedIn = a.ClockedIn,
        ClockedInAt = a.ClockedInAt,
        ClockedOut = a.ClockedOut,
        ClockedOutAt = a.ClockedOutAt,
    };

    public override CreateForUserResponse FromEntity(Attendance e) => new()
    {
        AttendaceRecordCreated = new AttendanceDto()
        {
            Id = e.Id,
            AttendanceDay = e.AttendanceDay,
            ClockedIn = e.ClockedIn,
            ClockedOut = e.ClockedOut,
            ClockedInAt = e.ClockedInAt,
            ClockedOutAt = e.ClockedOutAt,
            UserId = e.UserId,
        }
    };
}
