using Ardalis.Result;
using Ardalis.SharedKernel;
using CleanArchAttendanceApp.Core.Entities;
using CleanArchAttendanceApp.Core.Interfaces;

namespace CleanArchAttendanceApp.UseCases.User.Command.ClockOut;
public class ClockOutCommandHandler : ICommandHandler<ClockOutCommand, Result>
{
  private readonly IAttendanceRepository _repository;

  public ClockOutCommandHandler(IAttendanceRepository repository)
  {
    _repository = repository;
  }

  public async Task<Result> Handle(ClockOutCommand req, CancellationToken ct)
  {
    var user = await _repository.GetUserByIdAsync(req.UserId);
    if (user == null)
      return Result.Error("user not found");

    var attendanceForToday =
            await _repository.GetAttendanceRecordForTodayByUserIDAsync(req.UserId);

    if (attendanceForToday == null
        || attendanceForToday != null && attendanceForToday.ClockedIn == false)
    {
      return Result.Error("pleas clock-in first");
    }
    else
    {
      attendanceForToday!.ClockedOut = true;
      attendanceForToday.ClockedOutAt = DateTime.Now;
      await _repository.SaveChangesAsync();
    }

    return Result.Success();
  }
}
