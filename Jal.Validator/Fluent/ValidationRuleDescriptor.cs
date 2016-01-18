using Jal.Factory.Model;
using Jal.Validator.Interface;

namespace Jal.Validator.Fluent
{
    public class ValidationRuleDescriptor<T>
    {
        private readonly ObjectFactoryConfigurationItem _objectFactoryConfigurationItem;

        public ValidationRuleDescriptor(ObjectFactoryConfigurationItem objectFactoryConfigurationItem)
        {
            _objectFactoryConfigurationItem = objectFactoryConfigurationItem;

            _objectFactoryConfigurationItem.TargetType = typeof(T);
        }

        public ValidationRuleWhenDescriptor<T> With<T2>() where T2 : IValidator<T>
        {
            _objectFactoryConfigurationItem.ResultType = typeof(T2);

            return new ValidationRuleWhenDescriptor<T>(_objectFactoryConfigurationItem);
        }
    } 
}
