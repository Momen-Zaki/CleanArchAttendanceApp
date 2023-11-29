using Ardalis.Result;
using Ardalis.SharedKernel;
using CleanArchAttendanceApp.Core.Entities;
using CleanArchAttendanceApp.Core.Interfaces;
using CleanArchAttendanceApp.Core.Models;

namespace CleanArchAttendanceApp.UseCases.User.Query.GetById;
public class GetUserByIdQueryHandler 
  : IQueryHandler<GetUserByIdQuery, Result<UserWithoutAttendanceDto>>
{
  private readonly IAttendanceRepository _repository;
  public GetUserByIdQueryHandler(IAttendanceRepository repository)
  {
    _repository = repository;
  }
  public async Task<Result<UserWithoutAttendanceDto>> Handle(GetUserByIdQuery req, CancellationToken ct)
  {
    var user = await _repository.GetUserByIdAsync(req.UserId);
    if (user == null)
      return Result.NotFound();

    var UserToSend = new UserWithoutAttendanceDto()
    {
      Id = user.Id,
      FullName = user.FullName,
      UserName = user.UserName,
      Role = user.Role,
    };

    return UserToSend;
  }
}
