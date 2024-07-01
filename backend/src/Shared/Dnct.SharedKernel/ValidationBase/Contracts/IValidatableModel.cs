using FluentValidation;

namespace Dnct.SharedKernel.ValidationBase.Contracts;

public interface IValidatableModel<TApplicationModel> where TApplicationModel:class
{
    IValidator<TApplicationModel> ValidateApplicationModel(ApplicationBaseValidationModelProvider<TApplicationModel> validator);
}