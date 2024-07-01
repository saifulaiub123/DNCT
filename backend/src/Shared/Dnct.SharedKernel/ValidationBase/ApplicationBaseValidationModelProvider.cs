using Dnct.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Dnct.SharedKernel.ValidationBase;

public class ApplicationBaseValidationModelProvider<TApplicationModel>:AbstractValidator<TApplicationModel>
{
    public IServiceScope ServiceProvider { get; }

    public ApplicationBaseValidationModelProvider(IServiceScope serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }
}