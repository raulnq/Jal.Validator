using System;
using Jal.Factory.Model;

namespace Jal.Validator.Fluent
{
    public class ValidationRuleWhenDescriptor<TTarget>
    {
        private readonly ObjectFactoryConfigurationItem _objectFactoryConfigurationItem;

        public ValidationRuleWhenDescriptor(ObjectFactoryConfigurationItem objectFactoryConfigurationItem)
        {
            _objectFactoryConfigurationItem = objectFactoryConfigurationItem;
        }
        public void When(Func<TTarget, bool> selector)
        {
            _objectFactoryConfigurationItem.Selector = selector;
        }
    }
}
