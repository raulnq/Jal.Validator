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

        private IValidator<T>[] Create<T>(T instance, string @group)
        {
            IValidator<T>[] validators;

            if (string.IsNullOrWhiteSpace(@group))
            {
                validators = ObjectFactory.Create<T, IValidator<T>>(instance);
            }
            else
            {
                validators = ObjectFactory.Create<T, IValidator<T>>(instance, @group);
            }

            if (validators != null)
            {
                return validators;
            }
            else
            {
                throw new ArgumentNullException(string.Format("It's not posible to get a validator instance of the rule {0}", @group));
            }
        }

        private ValidationResult Validate<T>(T instance, string @group, Func<IValidator<T>, ValidationResult> validate)
        {
            var validators = Create(instance, @group);

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

        public ValidationResult Validate<T>(T instance, string @group , dynamic context)
        {
            var validators = Create(instance, @group);

            foreach (var validator in validators)
            {
                var contextValidator = validator as ITransientValidator;

                if (contextValidator != null)
                {
                    contextValidator.Context = context;

                    contextValidator.Group = @group;

                    contextValidator.Subgroup = string.Empty;
                }

                var result = validator.Validate(instance, context);

                if (!result.IsValid)
                {
                    return result;
                }
            }

            return new ValidationResult();
        }

        public ValidationResult Validate<T>(T instance, string @group, string subgroup, dynamic context)
        {
            var validators = Create(instance, @group);

            foreach (var validator in validators)
            {
                var contextValidator = validator as ITransientValidator;

                if (contextValidator != null)
                {
                    contextValidator.Context = context;

                    contextValidator.Group = @group;

                    contextValidator.Subgroup = subgroup;
                }

                var result = validator.Validate(instance, subgroup, context);

                if (!result.IsValid)
                {
                    return result;
                }
            }

            return new ValidationResult();
        }

        public ValidationResult Validate<T>(T instance, string @group)
        {
            return Validate(instance, @group, x => x.Validate(instance));
        }

        public ValidationResult Validate<T>(T instance, string @group, string subgroup)
        {
            return Validate(instance, @group, x => x.Validate(instance, subgroup));
        }

        #endregion
    }
}
