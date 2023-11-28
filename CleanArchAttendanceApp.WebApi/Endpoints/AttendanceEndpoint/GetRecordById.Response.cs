using CleanArchAttendanceApp.Core.Models;

namespace CleanArchAttendanceApp.WebApi.Endpoints.AttendanceEndpoint;

public class GetRecordByIdResponse
{
    public AttendanceDto? Record { get; set; }
}
