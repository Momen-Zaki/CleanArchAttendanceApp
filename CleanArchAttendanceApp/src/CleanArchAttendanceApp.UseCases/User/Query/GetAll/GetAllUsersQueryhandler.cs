using Ardalis.Result;
using Ardalis.SharedKernel;
using CleanArchAttendanceApp.Core.Interfaces;
using CleanArchAttendanceApp.Core.Models;

namespace CleanArchAttendanceApp.UseCases.User.Query.GetAll;
public class GetAllUsersQueryhandler
  : IQueryHandler<GetAllUsersQuery, Result<List<UserWithoutAttendanceDto>>>
{
  private readonly IAttendanceRepository _repository;

  public GetAllUsersQueryhandler(IAttendanceRepository repository)
  {
    _repository = repository;
  }

  public async Task<Result<List<UserWithoutAttendanceDto>>>
    Handle(GetAllUsersQuery req, CancellationToken ct)
  {
    var entities = await _repository.GetAllUsersAsync();
    var users = new List<UserWithoutAttendanceDto>();
    foreach (var e in entities)
    {
      users.Add(new UserWithoutAttendanceDto()
      {
        Id = e.Id,
        FullName = e.FullName,
        UserName = e.UserName,
        Role = e.Role,
      });
    }
    return users;
  }
}
