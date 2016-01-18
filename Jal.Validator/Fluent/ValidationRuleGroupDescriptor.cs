using System.Collections.Generic;
using Jal.Factory.Model;
using Jal.Validator.Interface;

namespace Jal.Validator.Fluent
{
    public class ValidationRuleGroupDescriptor<TTarget>
    {
        private readonly List<ObjectFactoryConfigurationItem> _objectFactoryConfigurationItems;

        private readonly string _groupName;

        public ValidationRuleGroupDescriptor(List<ObjectFactoryConfigurationItem> objectFactoryConfigurationItems, string groupName)
        {
            _objectFactoryConfigurationItems = objectFactoryConfigurationItems;

            _groupName = groupName;
        }

        public ValidationRuleWhenDescriptor<TTarget> With<TValidator>() where TValidator : IValidator<TTarget>
        {
            var value = new ObjectFactoryConfigurationItem(typeof(TTarget)) { ResultType = typeof(TValidator), GroupName = _groupName };

            _objectFactoryConfigurationItems.Add(value);

            return new ValidationRuleWhenDescriptor<TTarget>(value);
        }
    }
}
