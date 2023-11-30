using Ardalis.Result;
using Ardalis.SharedKernel;
using CleanArchAttendanceApp.Core.Models;

namespace CleanArchAttendanceApp.UseCases.User.Command.Delete;
public record DeleteUserCommand(Guid userId) : ICommand<Result>;
