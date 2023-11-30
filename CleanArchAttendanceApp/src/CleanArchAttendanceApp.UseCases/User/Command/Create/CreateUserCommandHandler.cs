using Ardalis.Result;
using Ardalis.SharedKernel;
using CleanArchAttendanceApp.Core.Interfaces;
using CleanArchAttendanceApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanArchAttendanceApp.UseCases.User.Command.Create;
public class CreateUserCommandHandler 
  : ICommandHandler<CreateUserCommand, Result<UserWithoutAttendanceDto>>
{
  private readonly IAttendanceRepository _reposityory;

  public CreateUserCommandHandler(IAttendanceRepository reposityory)
  {
    _reposityory = reposityory;
  }

  public async Task<Result<UserWithoutAttendanceDto>> Handle(CreateUserCommand req, CancellationToken ct)
  {
    // need more work
    if (req.role!= UserRole.Employee && req.role != UserRole.Admin)
      return Result.Error("Invalid User Role!");

    var hashedPassword = BCrypt.Net.BCrypt.HashPassword(req.password);

    if (await _reposityory.GetUserByUserNameAsync(req.username!) != null)
      return Result.Error("username is taken, try another one!");

    await _reposityory.AddUser(req.fullname,req.username!, hashedPassword, req.role);

    var userCreated = await _reposityory.GetUserByUserNameAsync(req.username);
    if (userCreated == null)
      return Result.Error("can't create a new user for now");

    var userToReturn = new UserWithoutAttendanceDto()
    {
      Id = userCreated.Id,
      FullName = userCreated.FullName,
      UserName = userCreated.UserName,
      Role = userCreated.Role,
    }; 

    return userToReturn;
  }
}
