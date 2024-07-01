using Dnct.Application.Models.Common;
using Mediator;

namespace Dnct.Application.Features.Role.Queries.GetAllRolesQuery;

public record GetAllRolesQuery():IRequest<OperationResult<List<GetAllRolesQueryResponse>>>;