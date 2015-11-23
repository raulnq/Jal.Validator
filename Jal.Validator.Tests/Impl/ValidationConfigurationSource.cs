using Jal.Validator.Impl;
using Jal.Validator.Tests.Model;

namespace Jal.Validator.Tests.Impl
{
    public class ValidationConfigurationSource : AbstractValidationConfigurationSource
    {
        public ValidationConfigurationSource()
        {
            Validate<Customer>().With<CustomerValidator>();
        }
    }
}
