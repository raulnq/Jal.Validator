using Jal.Factory.Model;
using Jal.Validator.Fluent.Interface;
using Jal.Validator.Interface;

namespace Jal.Validator.Fluent.Impl
{
    public class ValidationRuleFluentBuilder<TTarget> : IValidationRuleFluentBuilder<TTarget>
    {
        private readonly ObjectFactoryConfigurationItem _objectFactoryConfigurationItem;

        public ValidationRuleFluentBuilder(ObjectFactoryConfigurationItem objectFactoryConfigurationItem)
        {
            _objectFactoryConfigurationItem = objectFactoryConfigurationItem;

            _objectFactoryConfigurationItem.TargetType = typeof(TTarget);
        }

        public IValidationRuleWhenFluentBuilder<TTarget> With<TValidator>() where TValidator : IValidator<TTarget>
        {
            _objectFactoryConfigurationItem.ResultType = typeof(TValidator);

            return new ValidationRuleWhenFluentBuilder<TTarget>(_objectFactoryConfigurationItem);
        }
    }
}
