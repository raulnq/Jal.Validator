using Jal.Validator.Fluent;

namespace Jal.Validator.Interface.Fluent
{
    public interface IValidationRuleGroupFluentBuilder<out TTarget>
    {
        IValidationRuleWhenFluentBuilder<TTarget> With<TValidator>() where TValidator : IValidator<TTarget>;
    }
}