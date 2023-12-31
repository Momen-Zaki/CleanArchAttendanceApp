﻿using Ardalis.Result;
using Ardalis.SharedKernel;
using CleanArchAttendanceApp.Core.Entities;
using CleanArchAttendanceApp.Core.Interfaces;
using CleanArchAttendanceApp.Core.Models;

namespace CleanArchAttendanceApp.UseCases.User.Query.GetByIdWithRecords;
public class GetUserWithRecordsQueryHandler
  : IQueryHandler<GetUserWithRecordsQuery, Result<UserDto>>
{
  private readonly IAttendanceRepository _repository;
  public GetUserWithRecordsQueryHandler(IAttendanceRepository repository)
  {
    _repository = repository;
  }
  public async Task<Result<UserDto>> Handle(GetUserWithRecordsQuery req, CancellationToken ct)
  {
    var user = await _repository.GetUserByIdAsync(req.UserId, true);
    if (user == null)
      return Result.NotFound();

    var UserToSend = new UserDto()
    {
      Id = user.Id,
      FullName = user.FullName,
      UserName = user.UserName,
      Role = user.Role,
    };

    foreach (var a in user.AttendanceRecords)
    {
      UserToSend.AttendanceRecords.Add(new()
      {
        Id = a.Id,
        AttendanceDay = a.AttendanceDay,
        ClockedInAt = a.ClockedInAt,
        ClockedIn = a.ClockedIn,
        ClockedOut = a.ClockedOut,
        ClockedOutAt = a.ClockedOutAt,
        UserId = a.UserId
      });
    }

    return UserToSend;
  }
}
