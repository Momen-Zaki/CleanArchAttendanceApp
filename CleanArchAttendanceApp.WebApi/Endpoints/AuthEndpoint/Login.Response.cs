using CleanArchAttendanceApp.Core.Models;

namespace CleanArchAttendanceApp.WebApi.Endpoints.AuthEndpoint;

public class LoginResponse
{
    public LoggedInCredentials? UserCredentials { get; set; }
}
