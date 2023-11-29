using Ardalis.Result;
using Ardalis.SharedKernel;
using CleanArchAttendanceApp.Core.Models;

namespace CleanArchAttendanceApp.UseCases.User.Query.GetAll;
public record GetAllUsersQuery() : IQuery<Result<List<UserWithoutAttendanceDto>>>;
