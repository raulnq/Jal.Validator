using System;

namespace Jal.Validator.Interface.Fluent
{
    public interface IValidationRuleWhenFluentBuilder<out TTarget>
    {
        void When(Func<TTarget, bool> selector);
    }
}