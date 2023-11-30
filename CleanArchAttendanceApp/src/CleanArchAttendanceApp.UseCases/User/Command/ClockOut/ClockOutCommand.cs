using Ardalis.Result;
using Ardalis.SharedKernel;

namespace CleanArchAttendanceApp.UseCases.User.Command.ClockOut;
public record ClockOutCommand(Guid UserId) : ICommand<Result>;
