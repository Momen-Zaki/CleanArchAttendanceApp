using Ardalis.Result;
using Ardalis.SharedKernel;
using CleanArchAttendanceApp.Core.Entities;
using CleanArchAttendanceApp.Core.Interfaces;
using CleanArchAttendanceApp.Core.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchAttendanceApp.UseCases.User.Command.Create;
public class DeleteUserCommandHandler
  : ICommandHandler<DeleteUserCommand, Result>
{
  private readonly IAttendanceRepository _repository;

  public DeleteUserCommandHandler(IAttendanceRepository repository)
  {
    _repository = repository;
  }

  public async Task<Result> Handle(DeleteUserCommand req, CancellationToken ct)
  {
    var user = await _repository.GetUserByIdAsync(req.userId);
    if (user == null)
      return Result.Error("user not found");

    if (!await _repository.DeleteUserAsync(user))
      return Result.Error("Can't delete user for now");

    return Result.Success();
  }
}
