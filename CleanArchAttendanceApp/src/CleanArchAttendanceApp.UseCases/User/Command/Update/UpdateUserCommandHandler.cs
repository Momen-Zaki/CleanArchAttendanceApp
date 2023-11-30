using Ardalis.Result;
using Ardalis.SharedKernel;
using CleanArchAttendanceApp.Core.Interfaces;
using CleanArchAttendanceApp.Core.Models;

namespace CleanArchAttendanceApp.UseCases.User.Command.Create;
public class UpdateUserCommandHandler
  : ICommandHandler<UpdateUserCommand, Result<UserWithoutAttendanceDto>>
{
  private readonly IAttendanceRepository _repository;

  public UpdateUserCommandHandler(IAttendanceRepository repository)
  {
    _repository = repository;
  }

  public async Task<Result<UserWithoutAttendanceDto>> Handle(UpdateUserCommand req, CancellationToken ct)
  {
    // need more work
    if (req.role != UserRole.Employee && req.role != UserRole.Admin)
      return Result.Error("Invalid User Role!");

    var user = await _repository.GetUserByIdAsync(req.userId);
    if (user == null)
      return Result.Error("User not found");

    user.FullName = req.fullname;
    user.UserName = req.username;
    user.Role = req.role;
    user.PasswrodHash = BCrypt.Net.BCrypt.HashPassword(req.password);

    if (!await _repository.SaveChangesAsync())
      return Result.Error("Can't update user for now!");

    var userToReturn = new UserWithoutAttendanceDto()
    {
      Id = user.Id,
      FullName = user.FullName,
      UserName = user.UserName,
      Role = user.Role,
    }; 

    return userToReturn;
  }
}
