using System;
using Jal.Factory.Model;
using Jal.Validator.Fluent.Interface;

namespace Jal.Validator.Fluent.Impl
{
    public class ValidationRuleWhenFluentBuilder<TTarget> : IValidationRuleWhenFluentBuilder<TTarget>
    {
        private readonly ObjectFactoryConfigurationItem _objectFactoryConfigurationItem;

        public ValidationRuleWhenFluentBuilder(ObjectFactoryConfigurationItem objectFactoryConfigurationItem)
        {
            _objectFactoryConfigurationItem = objectFactoryConfigurationItem;
        }
        public void When(Func<TTarget, bool> selector)
        {
            _objectFactoryConfigurationItem.Selector = selector;
        }
    }
}
