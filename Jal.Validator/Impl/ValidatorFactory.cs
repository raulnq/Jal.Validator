using System;
using Jal.Factory.Interface;
using Jal.Validator.Interface;

namespace Jal.Validator.Impl
{
    public class ValidatorFactory : IValidatorFactory
    {
        protected readonly IObjectFactory ObjectFactory;

        public ValidatorFactory(IObjectFactory objectFactory)
        {
            ObjectFactory=objectFactory;
        }

        public IValidator<T>[] Create<T>(T instance, string validationgroup)
        {
            IValidator<T>[] validators;

            if (string.IsNullOrWhiteSpace(validationgroup))
            {
                validators = ObjectFactory.Create<T, IValidator<T>>(instance);
            }
            else
            {
                validators = ObjectFactory.Create<T, IValidator<T>>(instance, validationgroup);
            }

            return validators;
        }
    }
}
