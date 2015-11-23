using System;
using Jal.Factory.Model;
using Jal.Validator.Interface;

namespace Jal.Validator.Model
{
    public class ValidationRuleDescriptor<T>
    {
        private readonly ObjectFactoryConfigurationItem _objectFactoryConfigurationItem;

        public ValidationRuleDescriptor(ObjectFactoryConfigurationItem objectFactoryConfigurationItem)
        {
            _objectFactoryConfigurationItem = objectFactoryConfigurationItem;

            _objectFactoryConfigurationItem.TargetType = typeof(T);
        }

        public ValidationRuleDescriptor<T> With<T2>() where T2 : IValidator<T>
        {
            _objectFactoryConfigurationItem.ResultType = typeof(T2);

            return this;
        }

        public ValidationRuleDescriptor<T> Name(string name)
        {
            _objectFactoryConfigurationItem.GroupName = name;

            return this;
        }

        public ValidationRuleDescriptor<T> When(Func<T, bool> selector)
        {
            _objectFactoryConfigurationItem.Selector = selector;

            return this;
        }
    } 
}
