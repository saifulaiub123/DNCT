﻿using Dnct.Application.Features.Identity.Queries.GenerateUserToken;
using Dnct.Application.Features.Identity.Queries.TokenRequest;
using Dnct.Web.Plugins.Grpc.ProtoModels;
using Grpc.Core;
using Mediator;

namespace Dnct.Web.Plugins.Grpc.Services;

public class UserGrpcServices : UserServices.UserServicesBase
{
    private readonly IMediator _mediator;

    public UserGrpcServices(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<TokenRequestResult> TokenRequest(UserTokenRequest request, ServerCallContext context)
    {
        if (request is null)
        {
            context.Status = new Status(StatusCode.InvalidArgument, "Required Arguments Not Found");

            return new TokenRequestResult() { IsSuccess = false, Message = "Input Model Not Found" };
        }

        var tokenQuery = await _mediator.Send(new UserTokenRequestQuery(request.PhoneNumber));

        if (!tokenQuery.IsSuccess)
        {
            context.Status = new Status(StatusCode.InvalidArgument, "User not found");

            return new TokenRequestResult()
            { IsSuccess = false, Message = tokenQuery.ErrorMessage, UserTokenRequestResult = null };
        }

        if (tokenQuery.IsNotFound)
        {
            context.Status = new Status(StatusCode.NotFound, "User Not Found");

            return new TokenRequestResult()
            { IsSuccess = false, Message = tokenQuery.ErrorMessage, UserTokenRequestResult = null };
        }

        return new TokenRequestResult()
        {
            IsSuccess = true,
            Message = string.Empty,
            UserTokenRequestResult = new TokenRequestResultModel() { UserKey = tokenQuery.Result.UserKey }
        };
    }

    public override async Task<GetUserTokenRequestResult> GetUserToken(GetUserTokenRequestModel request, ServerCallContext context)
    {

        if (request is null)
        {
            context.Status = new Status(StatusCode.InvalidArgument, "Required Arguments Not Found");

            return new GetUserTokenRequestResult() { IsSuccess = false, Message = "Input Model Not Found" };
        }

        var tokenQuery = await _mediator.Send(new GenerateUserTokenQuery(request.UserKey, request.Code));

        if (!tokenQuery.IsSuccess)
        {
            context.Status = new Status(StatusCode.InvalidArgument, tokenQuery.ErrorMessage);

            return new GetUserTokenRequestResult() { IsSuccess = false, Message = tokenQuery.ErrorMessage };
        }


        return new GetUserTokenRequestResult()
        {
            IsSuccess = true, Message = string.Empty,
            Token = new UserToken()
            {
                AccessToken = tokenQuery.Result.AccessToken, ExpiresIn = tokenQuery.Result.ExpiresIn,
                RefreshToken = tokenQuery.Result.RefreshToken, TokenType = tokenQuery.Result.TokenType
            }
        };
    }
}