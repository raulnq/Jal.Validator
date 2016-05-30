using Jal.Validator.Fluent;

namespace Jal.Validator.Interface.Fluent
{
    public interface IValidationRuleFluentBuilder<out TTarget>
    {
        IValidationRuleWhenFluentBuilder<TTarget> With<TValidator>() where TValidator : IValidator<TTarget>;
    }
}