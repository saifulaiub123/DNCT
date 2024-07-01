
using Dnct.Application.Models.Common;
using Mediator;

namespace Dnct.Application.Features.Role.Commands.UpdateRoleClaimsCommand;

public record UpdateRoleClaimsCommand( int RoleId, List<string> RoleClaimValue):IRequest<OperationResult<bool>>;