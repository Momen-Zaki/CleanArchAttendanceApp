using Ardalis.Result;
using Ardalis.SharedKernel;
using CleanArchAttendanceApp.Core.Models;

namespace CleanArchAttendanceApp.UseCases.User.Query.GetByIdWithRecords;

public record GetUserWithRecordsQuery(Guid UserId) : IQuery<Result<UserDto>>;
