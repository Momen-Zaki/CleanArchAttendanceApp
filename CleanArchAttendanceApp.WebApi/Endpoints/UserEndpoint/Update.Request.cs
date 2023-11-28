namespace CleanArchAttendanceApp.WebApi.Endpoints.UserEndpoint;

public class UpdateRequest
{
    public string? FullName { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? Role { get; set; }
}
