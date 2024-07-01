using Dnct.Application.Models.Common;
using Mediator;

namespace Dnct.Application.Features.Order.Queries.GetUserOrders;

public record GetUserOrdersQueryModel(int UserId) : IRequest<OperationResult<List<GetUsersQueryResultModel>>>;