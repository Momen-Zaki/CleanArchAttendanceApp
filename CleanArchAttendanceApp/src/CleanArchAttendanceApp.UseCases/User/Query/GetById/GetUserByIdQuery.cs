using Ardalis.Result;
using Ardalis.SharedKernel;
using CleanArchAttendanceApp.Core.Models;

namespace CleanArchAttendanceApp.UseCases.User.Query.GetById;

public record GetUserByIdQuery(Guid UserId) : IQuery<Result<UserWithoutAttendanceDto>>;
