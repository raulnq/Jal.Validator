using System;
using Jal.Factory.Interface;
using Jal.Validator.Interface;
using Jal.Validator.Model;

namespace Jal.Validator.Impl
{
    public class ModelValidator : IModelValidator
    {
        protected readonly IObjectFactory ObjectFactory;

        public ModelValidator(IObjectFactory objectFactory)
        {
            ObjectFactory=objectFactory;
        }

        private IValidator<T>[] Create<T>(T instance, string rulename)
        {
            IValidator<T>[] validators;

            if (string.IsNullOrWhiteSpace(rulename))
            {
                validators = ObjectFactory.Create<T, IValidator<T>>(instance);
            }
            else
            {
                validators = ObjectFactory.Create<T, IValidator<T>>(instance, rulename);
            }

            if (validators != null)
            {
                return validators;
            }
            else
            {
                throw new ArgumentNullException(string.Format("It's not posible to get a validator instance of the rule {0}", rulename));
            }
        }

        private ValidationResult Validate<T>(T instance, string rulename, Func<IValidator<T>, ValidationResult> validate)
        {
            var validators = Create(instance, rulename);

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

        public ValidationResult Validate<T>(T instance, string rulename , dynamic context)
        {
            var validators = Create(instance, rulename);

            foreach (var validator in validators)
            {
                var contextValidator = validator as IValidatorContextContainer;

                if (contextValidator != null)
                {
                    contextValidator.Context = context;
                }

                var result = validator.Validate(instance, context);

                if (!result.IsValid)
                {
                    return result;
                }
            }

            return new ValidationResult();
        }

        public ValidationResult Validate<T>(T instance, string rulename, string ruleset, dynamic context)
        {
            var validators = Create(instance, rulename);

            foreach (var validator in validators)
            {
                var contextValidator = validator as IValidatorContextContainer;

                if (contextValidator != null)
                {
                    contextValidator.Context = context;
                }

                var result = validator.Validate(instance, ruleset, context);

                if (!result.IsValid)
                {
                    return result;
                }
            }

            return new ValidationResult();
        }

        public ValidationResult Validate<T>(T instance, string rulename)
        {
            return Validate(instance, rulename, x => x.Validate(instance));
        }

        public ValidationResult Validate<T>(T instance, string rulename, string ruleset)
        {
            return Validate(instance, rulename, x => x.Validate(instance, ruleset));
        }

        #endregion
    }
}
