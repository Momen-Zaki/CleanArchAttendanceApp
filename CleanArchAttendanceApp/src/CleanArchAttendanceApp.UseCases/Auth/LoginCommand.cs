using Ardalis.Result;
using Ardalis.SharedKernel;
using CleanArchAttendanceApp.Core.Models;

namespace CleanArchAttendanceApp.UseCases.Auth;

public record LoginCommand(string username, string password) 
  : ICommand<Result<LoggedInCredentials>>;
