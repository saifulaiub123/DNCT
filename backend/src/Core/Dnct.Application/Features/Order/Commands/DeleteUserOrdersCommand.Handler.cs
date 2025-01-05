//using Dnct.Application.Contracts.Persistence;
//using Dnct.Application.Models.Common;
//using Mediator;

//namespace Dnct.Application.Features.Order.Commands;

//public class DeleteUserOrdersCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserOrdersCommand,OperationResult<bool>>
//{
//    public async ValueTask<OperationResult<bool>> Handle(DeleteUserOrdersCommand request, CancellationToken cancellationToken)
//    {
//        await unitOfWork.OrderRepository.DeleteUserOrdersAsync(request.UserId);

//        return OperationResult<bool>.SuccessResult(true);
//    }
//}