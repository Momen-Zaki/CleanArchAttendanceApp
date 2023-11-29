using Ardalis.Result;
using Autofac;
using CleanArchAttendanceApp.Core.Models;
using CleanArchAttendanceApp.UseCases.User.Query.GetById;
using MediatR;

namespace CleanArchAttendanceApp.UseCases;
public class UseCasesModule: Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<GetUserByIdQueryHandler>()
      .As<IRequestHandler<GetUserByIdQuery, Result<UserWithoutAttendanceDto>>>();
  }
}
