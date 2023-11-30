using Ardalis.Result;
using Ardalis.SharedKernel;
using CleanArchAttendanceApp.Core.Models;

namespace CleanArchAttendanceApp.UseCases.User.Command.Create;
public record UpdateUserCommand(
  Guid userId,
  string fullname,
  string username,
  string password,
  string role ) : ICommand<Result<UserWithoutAttendanceDto>>;
