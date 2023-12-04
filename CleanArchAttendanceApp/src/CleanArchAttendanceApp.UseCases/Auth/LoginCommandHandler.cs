using System.Security.Claims;
using Ardalis.Result;
using Ardalis.SharedKernel;
using CleanArchAttendanceApp.Core.Interfaces;
using CleanArchAttendanceApp.Core.Models;
using FastEndpoints.Security;
using Microsoft.Extensions.Configuration;

namespace CleanArchAttendanceApp.UseCases.Auth;
public class LoginCommandHandler : ICommandHandler<LoginCommand, Result<LoggedInCredentials>>
{
  private readonly IConfiguration _configuration;
  private readonly IAttendanceRepository _repository;

  public LoginCommandHandler(IConfiguration configuration, IAttendanceRepository repository)
  {
    _configuration = configuration;
    _repository = repository;
  }

  public async Task<Result<LoggedInCredentials>> Handle(LoginCommand req, CancellationToken ct)
  {
    var passwordMatches = false;
    var user = await _repository.GetUserByUserNameAsync(req.username);
    if (user == null)
    {
      return Result.Invalid(new List<ValidationError>
      {
        new(){ErrorCode = "400", ErrorMessage = "Invaild Credentials"},
      });
    }
    else
    {
      if (!passwordMatches == BCrypt.Net.BCrypt.Verify(req.password, user!.PasswrodHash))
        return Result.Invalid(new List<ValidationError>
        {
          new(){ErrorCode = "400", ErrorMessage = "Invaild Credentials"},
        });

    }


    var role = UserRole.Employee;
    if (user!.Role == UserRole.Admin)
      role = UserRole.Admin;

    var token = JWTBearer.CreateToken(
           signingKey: _configuration["JwtBearerDefaults:SecretKey"]!,
           expireAt: DateTime.UtcNow.AddDays(1),
           privileges: u =>
           {
             u.Roles.Add(role);
             u.Claims.Add(
                       new Claim("UserName", user.UserName!),
                       new Claim("Role", user.Role!),
                       new Claim("Id", user.Id.ToString())
                       );
           });

    var userCredentials =  new LoggedInCredentials() 
    {
      UserName  = user.UserName,
      Token = token
    };

    return userCredentials;
  }
}
