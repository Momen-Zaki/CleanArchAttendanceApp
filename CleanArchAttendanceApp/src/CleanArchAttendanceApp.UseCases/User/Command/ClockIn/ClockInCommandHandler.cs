using Ardalis.Result;
using Ardalis.SharedKernel;
using CleanArchAttendanceApp.Core.Entities;
using CleanArchAttendanceApp.Core.Interfaces;
using CleanArchAttendanceApp.Core.Models;

namespace CleanArchAttendanceApp.UseCases.User.Command.ClockIn;
public class ClockInCommandHandler : ICommandHandler<ClockInCommand, Result>
{
  private readonly IAttendanceRepository _repository;

  public ClockInCommandHandler(IAttendanceRepository repository)
  {
    _repository = repository;
  }

  public async Task<Result> Handle(ClockInCommand req, CancellationToken ct)
  {
    var user = await _repository.GetUserByIdAsync(req.UserId);
    if (user == null)
      return Result.Error("user not found");

    var attendanceForToday =
        await _repository.GetAttendanceRecordForTodayByUserIDAsync(req.UserId);
    if (attendanceForToday == null)
    {
      await _repository.AddAttendanceRecordForClockIn(req.UserId);
    }
    else
    {
      attendanceForToday.ClockedIn = true;
      attendanceForToday.ClockedInAt = DateTime.Now;
      await _repository.SaveChangesAsync();
    }

    return Result.Success();
  }
}
