using Ardalis.Result;
using Ardalis.SharedKernel;
using CleanArchAttendanceApp.Core.Models;

namespace CleanArchAttendanceApp.UseCases.Attendance.Query.GetAllForUer;
public record GetAllForUserQuery(Guid userId) : IQuery<Result<List<AttendanceDto>>>;
