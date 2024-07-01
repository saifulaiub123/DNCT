using Dnct.Application.Models.Common;
using Mediator;

namespace Dnct.Application.Features.Order.Queries.GetAllOrders;

public record GetAllOrdersQuery():IRequest<OperationResult<List<GetAllOrdersQueryResult>>>;