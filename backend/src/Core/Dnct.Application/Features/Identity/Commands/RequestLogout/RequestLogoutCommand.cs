using Dnct.Application.Models.Common;
using Mediator;

namespace Dnct.Application.Features.Identity.Commands.RequestLogout;

public record RequestLogoutCommand(int UserId):IRequest<OperationResult<bool>>;