using Dnct.Application.Models.Common;
using Mediator;

namespace Dnct.Application.Features.Order.Commands;

public record DeleteUserOrdersCommand(int UserId):IRequest<OperationResult<bool>>;