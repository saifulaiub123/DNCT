using Dnct.Application.Models.Common;
using Mediator;

namespace Dnct.Application.Features.Users.Queries.GetUsers;

public record GetUsersQuery : IRequest<OperationResult<List<GetUsersQueryResponse>>>;