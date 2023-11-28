using CleanArchAttendanceApp.Core.Entities;
using CleanArchAttendanceApp.Core.Interfaces;
using CleanArchAttendanceApp.Core.Models;
using FastEndpoints;
using FastEndpoints.Security;
using System.Security.Claims;
using BCrypt;

namespace CleanArchAttendanceApp.WebApi.Endpoints.AuthEndpoint;

public class Login : Endpoint<LoginRequest, LoginResponse>
{
    private readonly IConfiguration configuration;
    private readonly IAttendanceRepository _repository;

    public Login(IConfiguration configuration, IAttendanceRepository repository)
    {
        this.configuration = configuration;
        _repository = repository;
    }

    public override void Configure()
    {
        Post("login");
        AllowAnonymous();
        Description(x => x.WithName("Login"));
        Summary(s =>
        {
            s.Summary = "Login";
            s.Description = "Return a Token for the give username and password";
            s.ExampleRequest = new LoginRequest()
            { Username = string.Empty, Password = string.Empty };
            s.ResponseExamples[200] = new LoginResponse()
            { UserName = string.Empty, Token = string.Empty };
            s.Responses[200] = "returns a Useraname and the Auth Token";
        });
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        var user = new User();
        var passwordMatches = false;
        try
        {
            user = await _repository.GetUserByUserNameAsync(req.Username!);
            passwordMatches = BCrypt.Net.BCrypt.Verify(req.Password, user.PasswrodHash);
        }
        catch (Exception)
        {
            ThrowError("user not found");
        }

        if (user == null || !passwordMatches)
            ThrowError("The supplied credentials are invalid!");

        var token = string.Empty;
        if (user.Role == UserRole.Admin)
        {
            token = JWTBearer.CreateToken(
                signingKey: configuration["JwtBearerDefaults:SecretKey"]!,
                expireAt: DateTime.UtcNow.AddDays(1),
                privileges: u =>
                {
                    u.Roles.Add("Admin");
                    u.Claims.Add(
                          new Claim("UserName", user.UserName!),
                          new Claim("Role", user.Role),
                          new Claim("Id", user.Id.ToString())
                          );
                });
        }
        else
        {
            token = JWTBearer.CreateToken(
                signingKey: configuration["JwtBearerDefaults:SecretKey"]!,
                expireAt: DateTime.UtcNow.AddDays(1),
                privileges: u =>
                {
                    u.Roles.Add("Employee");
                    u.Claims.Add(
                          new Claim("UserName", user.UserName!),
                          new Claim("Role", user.Role!),
                          new Claim("Id", user.Id.ToString())
                          );
                });
        }

        Response.UserName = req.Username;
        Response.Token = token;
        await SendAsync(Response, cancellation: ct);
    }
}
