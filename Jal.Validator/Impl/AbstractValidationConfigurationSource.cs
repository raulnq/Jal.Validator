using System.Collections.Generic;
using Jal.Factory.Interface;
using Jal.Factory.Model;
using Jal.Validator.Model;

namespace Jal.Validator.Impl
{
    public abstract class AbstractValidationConfigurationSource : IObjectFactoryConfigurationSource
    {
        private readonly List<ObjectFactoryConfigurationItem> _objectFactoryConfigurationItems = new List<ObjectFactoryConfigurationItem>();

        public ObjectFactoryConfiguration Source()
        {
            var result = new ObjectFactoryConfiguration();

            foreach (var item in _objectFactoryConfigurationItems)
            {
                result.Items.Add(item);
            }

            return result;
        }

        public ValidationRuleDescriptor<T> Validate<T>()
        {
            var value = new ObjectFactoryConfigurationItem(typeof(T));

            var descriptor = new ValidationRuleDescriptor<T>(value);
            
            _objectFactoryConfigurationItems.Add(value);

            return descriptor;
        }
    }     
}
