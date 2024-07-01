using Dnct.Application.Models.Common;
using Mediator;

namespace Dnct.Application.Features.Role.Queries.GetAuthorizableRoutesQuery;

public record GetAuthorizableRoutesQuery():IRequest<OperationResult<List<GetAuthorizableRoutesQueryResponse>>>;