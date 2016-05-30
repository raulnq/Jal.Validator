using System.Collections.Generic;
using Jal.Factory.Model;
using Jal.Validator.Interface;
using Jal.Validator.Interface.Fluent;

namespace Jal.Validator.Fluent
{
    public class ValidationRuleGroupFluentBuilder<TTarget> : IValidationRuleGroupFluentBuilder<TTarget>
    {
        private readonly List<ObjectFactoryConfigurationItem> _objectFactoryConfigurationItems;

        private readonly string _groupName;

        public ValidationRuleGroupFluentBuilder(List<ObjectFactoryConfigurationItem> objectFactoryConfigurationItems, string groupName)
        {
            _objectFactoryConfigurationItems = objectFactoryConfigurationItems;

            _groupName = groupName;
        }

        public IValidationRuleWhenFluentBuilder<TTarget> With<TValidator>() where TValidator : IValidator<TTarget>
        {
            var value = new ObjectFactoryConfigurationItem(typeof(TTarget)) { ResultType = typeof(TValidator), GroupName = _groupName };

            _objectFactoryConfigurationItems.Add(value);

            return new ValidationRuleWhenFluentBuilder<TTarget>(value);
        }
    }
}
