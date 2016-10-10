using Jal.Validator.Impl;
using Jal.Validator.Tests.Model;

namespace Jal.Validator.Tests.Impl
{
    public class ValidationConfigurationSource : AbstractValidationConfigurationSource
    {
        public ValidationConfigurationSource()
        {
 
        }
    }

    public class AutoValidationConfigurationSource : AbstractValidationConfigurationSource
    {
        public AutoValidationConfigurationSource()
        {
            Validate<Customer>().With<CustomerValidator>();
            Validate<Customer>("Group", x =>
            {
                x.With<CustomerValidator>();
                x.With<CustomerValidator>();
            });
        }
    }
}
