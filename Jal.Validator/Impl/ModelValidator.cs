using System;
using Jal.Validator.Fluent;
using Jal.Validator.Interface;
using Jal.Validator.Model;

namespace Jal.Validator.Impl
{
    public class ModelValidator : IModelValidator
    {
        public ModelValidator(IValidatorFactory validatorFactory)
        {
            Factory = validatorFactory;
        }

        public static IModelValidator Current;

        public static IValidatorFactorySetupDescriptor Setup
        {
            get
            {
                return new ModelValidatorSetupDescriptor();
            }
        }

        private ValidationResult Validate<T>(T instance, string validationgroup, Func<IValidator<T>, ValidationResult> validate)
        {
            var validators = Factory.Create(instance, validationgroup);

            foreach (var validator in validators)
            {
                var result = validate(validator);

                if (!result.IsValid)
                {
                    return result;
                }
            } 

            return new ValidationResult();
        }

        #region IModelValidator Members

        public ValidationResult Validate<T>(T instance)
        {
            return Validate(instance, string.Empty);
        }

        public ValidationResult Validate<T>(T instance, dynamic context)
        {
            return Validate(instance, string.Empty, context);
        }

        public ValidationResult Validate<T>(T instance, string validationgroup , dynamic context)
        {
            var validators = Factory.Create(instance, validationgroup);

            foreach (var validator in validators)
            {
                var result = validator.Validate(instance, context);

                if (!result.IsValid)
                {
                    return result;
                }
            }

            return new ValidationResult();
        }

        public ValidationResult Validate<T>(T instance, string validationgroup, string validationsubgroup, dynamic context)
        {
            var validators = Factory.Create(instance, validationgroup);

            foreach (var validator in validators)
            {
                var result = validator.Validate(instance, validationsubgroup, context);

                if (!result.IsValid)
                {
                    return result;
                }
            }

            return new ValidationResult();
        }

        public IValidatorFactory Factory { get; set; }

        public ValidationResult Validate<T>(T instance, string validationgroup)
        {
            return Validate(instance, validationgroup, x => x.Validate(instance));
        }

        public ValidationResult Validate<T>(T instance, string validationgroup, string validationsubgroup)
        {
            return Validate(instance, validationgroup, x => x.Validate(instance, validationsubgroup));
        }

        #endregion
    }
}
