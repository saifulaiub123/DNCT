﻿using System.Security.Claims;
using Carter;
using Dnct.Application.Features.Identity.Commands.Create;
using Dnct.Application.Features.Identity.Commands.RefreshUserTokenCommand;
using Dnct.Application.Features.Identity.Commands.RequestLogout;
using Dnct.Application.Features.Identity.Queries.GenerateUserToken;
using Dnct.Application.Features.Identity.Queries.TokenRequest;
using Dnct.SharedKernel.Extensions;
using Dnct.WebFramework.WebExtensions;
using Mediator;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;

namespace Dnct.Web.Api.Endpoints;

public class UserEndpoints : ICarterModule
{
    private readonly string _routePrefix= "/api/v{version:apiVersion}/Users/";
    private readonly double _version = 1.1;
    private readonly string _tag ="User";
    public void AddRoutes(IEndpointRouteBuilder app)
    { 
        app.MapEndpoint(
            builder => builder.MapPost($"{_routePrefix}Register", async (UserCreateCommand model,ISender sender) =>
        {
            var result = await sender.Send(model);
            return result.ToEndpointResult();
        }),_version,"Register",_tag);


        app.MapEndpoint(
            builder => builder.MapPost($"{_routePrefix}TokenRequest", async (UserTokenRequestQuery model, ISender sender) =>
            {
                var result = await sender.Send(model);
                return result.ToEndpointResult();
            }), _version, "TokenRequest", _tag);


        app.MapEndpoint(
            builder => builder.MapPost($"{_routePrefix}LoginConfirmation", async (GenerateUserTokenQuery model, ISender sender) =>
            {
                var result = await sender.Send(model);
                return result.ToEndpointResult();
            }), _version, "LoginConfirmation", _tag);

        app.MapEndpoint(
            builder => builder.MapGet($"{_routePrefix}RefreshSignIn", async ( Guid userRefreshToken, ISender sender) =>
            {

                var result = await sender.Send(new RefreshUserTokenCommand(userRefreshToken));
                return result.ToEndpointResult();
            }), _version, "RefreshSignIn", _tag);

        app.MapEndpoint(
            builder => builder.MapGet($"{_routePrefix}Logout", async (ClaimsPrincipal user, ISender sender) =>
            {

                var result = await sender.Send(new RequestLogoutCommand(int.Parse(user.Identity.GetUserId())));
                return result.ToEndpointResult();
            }), _version, "Logout", _tag)
            .RequireAuthorization();
    }
}