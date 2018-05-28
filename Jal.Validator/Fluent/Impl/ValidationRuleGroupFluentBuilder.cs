using System.Collections.Generic;
using Jal.Factory.Model;
using Jal.Validator.Fluent.Interface;
using Jal.Validator.Interface;

namespace Jal.Validator.Fluent.Impl
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
            var value = new ObjectFactoryConfigurationItem(typeof(TTarget)) { ResultType = typeof(TValidator), Name = _groupName };

            _objectFactoryConfigurationItems.Add(value);

            return new ValidationRuleWhenFluentBuilder<TTarget>(value);
        }
    }
}
