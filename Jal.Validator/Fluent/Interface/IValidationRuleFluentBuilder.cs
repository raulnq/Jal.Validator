using Jal.Validator.Interface;

namespace Jal.Validator.Fluent.Interface
{
    public interface IValidationRuleFluentBuilder<out TTarget>
    {
        IValidationRuleWhenFluentBuilder<TTarget> With<TValidator>() where TValidator : IValidator<TTarget>;
    }
}