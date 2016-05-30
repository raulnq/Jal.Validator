using System;
using System.Collections.Generic;
using Jal.Factory.Fluent;
using Jal.Factory.Interface;
using Jal.Factory.Model;
using Jal.Validator.Fluent;
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

        public ValidationRuleFluentBuilder<TTarget> Validate<TTarget>()
        {
            var value = new ObjectFactoryConfigurationItem(typeof(TTarget));

            var descriptor = new ValidationRuleFluentBuilder<TTarget>(value);
            
            _objectFactoryConfigurationItems.Add(value);

            return descriptor;
        }

        public void Validate<TTarget>(string name, Action<ValidationRuleGroupFluentBuilder<TTarget>> action)
        {
            var descriptor = new ValidationRuleGroupFluentBuilder<TTarget>(_objectFactoryConfigurationItems, name);

            action(descriptor);
        }
    }     
}
