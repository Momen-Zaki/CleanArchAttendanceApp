using Ardalis.Result;
using Ardalis.SharedKernel;
using CleanArchAttendanceApp.Core.Entities;
using CleanArchAttendanceApp.Core.Interfaces;
using CleanArchAttendanceApp.Core.Models;
using FastEndpoints;

namespace CleanArchAttendanceApp.UseCases.Attendance.Query.GetAllForUer;
public class GetAllForUserCommandHandler
  : IQueryHandler<GetAllForUserQuery, Result<List<AttendanceDto>>>
{
  private readonly IAttendanceRepository _repository;

  public GetAllForUserCommandHandler(IAttendanceRepository repository)
  {
    _repository = repository;
  }

  public async Task<Result<List<AttendanceDto>>> Handle(GetAllForUserQuery req, CancellationToken ct)
  {
    var user = await _repository.GetUserByIdAsync(req.userId, true);
    if (user == null)
      Result.Error("user not found");

    var attendancRecords =
        await _repository.GetAllAttendanceRecordsByUserIdAsync(user!.Id);
    var result = new List<AttendanceDto>();
    foreach (var a in attendancRecords)
    {
      result.Add(new AttendanceDto()
      {
        Id = a.Id,
        AttendanceDay = a.AttendanceDay,
        ClockedIn = a.ClockedIn,
        ClockedInAt = a.ClockedInAt,
        ClockedOut = a.ClockedOut,
        ClockedOutAt = a.ClockedOutAt,
        UserId = a.UserId
      });
    }
    return result;
  }
}
