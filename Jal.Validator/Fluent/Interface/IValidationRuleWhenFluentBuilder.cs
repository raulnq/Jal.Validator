using System;

namespace Jal.Validator.Fluent.Interface
{
    public interface IValidationRuleWhenFluentBuilder<out TTarget>
    {
        void When(Func<TTarget, bool> selector);
    }
}