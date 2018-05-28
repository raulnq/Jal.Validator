using System;
using Jal.Factory.Impl;
using Jal.Factory.Interface;
using Jal.Locator.Interface;
using Jal.Validator.Interface;
using Jal.Validator.Model;

namespace Jal.Validator.Impl
{
    public class ModelValidator : IModelValidator
    {
        public ModelValidator(IValidatorFactory validatorFactory)
        {
            Factory = validatorFactory;

            Interceptor = AbstractModelValidatorInterceptor.Instance;
        }

        public static IModelValidator Current;

        public static IModelValidator Create(IObjectFactoryConfigurationSource[] sources, IServiceLocator locator)
        {
            return new ModelValidator(new ValidatorFactory(new ObjectFactory(new ObjectFactoryConfigurationProvider(sources), new ObjectCreator(locator)))); 
        }

        public IModelValidatorInterceptor Interceptor { get; set; }

        ValidationResult Try<T>(T instance,  Func<IValidator<T>[]> creation, Func<IValidator<T>, ValidationResult> validation)
        {
            Interceptor.OnEnter(instance);
            try
            {
                var validators = creation();

                if (validators != null)
                {
                    foreach (var validator in validators)
                    {
                        var result = validation(validator);

                        if (!result.IsValid)
                        {
                            Interceptor.OnSuccess(instance, result);

                            return result;
                        }
                    }
                }

                return new ValidationResult();
            }
            catch (Exception ex)
            {
                Interceptor.OnError(instance, ex);

                throw;
            }
            finally
            {
                Interceptor.OnExit(instance);
            }
        }

        #region IModelValidator Members

        public ValidationResult Validate<T>(T instance)
        {
            return Try(instance, () => Factory.Create(instance, string.Empty), validator => validator.Validate(instance));
        }

        public ValidationResult Validate<T>(T instance, dynamic context)
        {
            return Try(instance, () => Factory.Create(instance, string.Empty), validator => validator.Validate(instance, context));
        }

        public ValidationResult Validate<T>(T instance, string validationgroup , dynamic context)
        {

            return Try(instance, () => Factory.Create(instance, validationgroup), validator => validator.Validate(instance, context));
            
        }

        public ValidationResult Validate<T>(T instance, string validationgroup, string validationsubgroup, dynamic context)
        {
            return Try(instance, () => Factory.Create(instance, validationgroup), validator => validator.Validate(instance, validationsubgroup, context));
        }

        public IValidatorFactory Factory { get; set; }

        public ValidationResult Validate<T>(T instance, string validationgroup)
        {
            return Try(instance, () => Factory.Create(instance, validationgroup), validator => validator.Validate(instance));

        }

        public ValidationResult Validate<T>(T instance, string validationgroup, string validationsubgroup)
        {
            return Try(instance, () => Factory.Create(instance, validationgroup), validator => validator.Validate(instance, validationsubgroup));
        }

        #endregion
    }
}
