using Ardalis.Result;
using CleanArchAttendanceApp.Core.Interfaces;
using CleanArchAttendanceApp.Core.Models;
using CleanArchAttendanceApp.UseCases.User.Query.GetAll;
using FastEndpoints;
using MediatR;

namespace CleanArchAttendanceApp.WebApi.Endpoints.UserEndpoint;

//public class GetAll : EndpointWithoutRequest<GetAllResponse, GetAllMapper>
public class GetAllUsers : EndpointWithoutRequest<GetAllResponse>
{
    private readonly IMediator _mediator;

    public GetAllUsers(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("users");
        Roles("Admin");
        Summary(s =>
        {
            s.Summary = "Get All Users";
            s.Description = "Get a list of all users";
            s.ResponseExamples[200] = new GetAllResponse
            {
                Users = new List<UserWithoutAttendanceDto>()
                    { new UserWithoutAttendanceDto()
                          { Id = Guid.NewGuid(),
                            FullName = string.Empty,
                            UserName = string.Empty,
                            Role = UserRole.Employee
                      },
                        new UserWithoutAttendanceDto()
                          { Id = Guid.NewGuid(),
                            FullName = string.Empty,
                            UserName = string.Empty,
                            Role = UserRole.Employee
                      },
                }
            };
            s.Responses[200] = "ok with a list of all useres";
            s.Responses[404] = "Can't delete it for now";
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var query = new GetAllUsersQuery();
        var result = await _mediator.Send(query);

        if (result.Status == ResultStatus.Unauthorized)
            ThrowError("you need to login first!");

        if (result.Status == ResultStatus.Forbidden)
            ThrowError("you shouldn't be here!");

        if (result.IsSuccess)
            Response.Users = result.Value;
    }
}
