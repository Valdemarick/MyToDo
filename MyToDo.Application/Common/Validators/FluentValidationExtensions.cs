using FluentValidation;
using MyToDo.Domain.Shared;

namespace MyToDo.Application.Common.Validators;

internal static class FluentValidationExtensions
{
    public static IRuleBuilderOptions<T, TProperty> WithCustomError<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> rule,
        Error error)
    {
        return rule.WithErrorCode(error.Code).WithMessage(error.Message);
    }
}
