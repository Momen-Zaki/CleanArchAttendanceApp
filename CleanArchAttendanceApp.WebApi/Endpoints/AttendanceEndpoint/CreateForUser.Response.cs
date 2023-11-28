using CleanArchAttendanceApp.Core.Models;

namespace CleanArchAttendanceApp.WebApi.Endpoints.AttendanceEndpoint;

public class CreateForUserResponse
{
    public AttendanceDto? AttendaceRecordCreated { get; set; }
}
