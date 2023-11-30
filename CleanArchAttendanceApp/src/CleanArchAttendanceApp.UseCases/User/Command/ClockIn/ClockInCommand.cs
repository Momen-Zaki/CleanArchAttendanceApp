using Ardalis.Result;
using Ardalis.SharedKernel;

namespace CleanArchAttendanceApp.UseCases.User.Command.ClockIn;
public record ClockInCommand(Guid UserId) : ICommand<Result>;
