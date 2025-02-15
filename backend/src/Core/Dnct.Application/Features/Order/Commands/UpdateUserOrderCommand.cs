﻿using System.Text.Json.Serialization;
using Dnct.Application.Models.Common;
using Dnct.SharedKernel.ValidationBase;
using Dnct.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;

namespace Dnct.Application.Features.Order.Commands;

public record UpdateUserOrderCommand
    (int OrderId, string OrderName) : IRequest<OperationResult<bool>>,IValidatableModel<UpdateUserOrderCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }

    public IValidator<UpdateUserOrderCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateUserOrderCommand> validator)
    {
        validator.RuleFor(c => c.OrderId).NotEmpty().GreaterThan(0);
        validator.RuleFor(c => c.OrderName).NotEmpty().NotNull();

        return validator;
    }
};