using Jal.Validator.Interface;
using Jal.Validator.Model;

namespace Jal.Validator.Extension
{
    public static class ValidatorExtension
    {
        public static bool IsValid<T>(this IValidator<T> validator, T objectTovalidate)
        {
            var result = validator.Validate(objectTovalidate);
            return result.IsValid;
        }

        public static bool IsValid<T>(this IValidator<T> validator, T objectTovalidate, dynamic context)
        {
            var result = validator.Validate(objectTovalidate, context);
            return result.IsValid;
        }

        public static bool IsValid<T>(this IValidator<T> validator, T objectTovalidate, string ruleset)
        {
            var result = validator.Validate(objectTovalidate, ruleset);
            return result.IsValid;
        }

        public static bool IsValid<T>(this IValidator<T> validator, T objectTovalidate, string ruleset, dynamic context)
        {
            var result = validator.Validate(objectTovalidate, ruleset, context);
            return result.IsValid;
        }

        public static void ShouldHaveValidationErrorFor<T>(this IValidator<T> validator, T objectTovalidate)
        {
           var result = validator.Validate(objectTovalidate);
            if (result.IsValid)
            {
                throw new ValidationException(result);
            }
        }

        public static void ShouldHaveValidationErrorFor<T>(this IValidator<T> validator, T objectTovalidate, dynamic context)
        {
            var result = validator.Validate(objectTovalidate, context);
            if (result.IsValid)
            {
                throw new ValidationException(result);
            }
        }

        public static void ShouldHaveValidationErrorFor<T>(this IValidator<T> validator, T objectTovalidate, string ruleset)
        {
            var result = validator.Validate(objectTovalidate, ruleset);
            if (result.IsValid)
            {
                throw new ValidationException(result);
            }
        }

        public static void ShouldHaveValidationErrorFor<T>(this IValidator<T> validator, T objectTovalidate, string ruleset, dynamic context)
        {
            var result = validator.Validate(objectTovalidate, ruleset, context);
            if (result.IsValid)
            {
                throw new ValidationException(result);
            }
        }


        public static void ShouldNotHaveValidationErrorFor<T>(this IValidator<T> validator, T objectTovalidate)
        {
            var result = validator.Validate(objectTovalidate);
            if (!result.IsValid)
            {
                throw new ValidationException(result);
            }
        }


        public static void ShouldNotHaveValidationErrorFor<T>(this IValidator<T> validator, T objectTovalidate, dynamic context)
        {
            var result = validator.Validate(objectTovalidate, context);
            if (!result.IsValid)
            {
                throw new ValidationException(result);
            }
        }

        public static void ShouldNotHaveValidationErrorFor<T>(this IValidator<T> validator, T objectTovalidate, string ruleset)
        {
            var result = validator.Validate(objectTovalidate, ruleset);
            if (!result.IsValid)
            {
                throw new ValidationException(result);
            }
        }

        public static void ShouldNotHaveValidationErrorFor<T>(this IValidator<T> validator, T objectTovalidate, string ruleset, dynamic context)
        {
            var result = validator.Validate(objectTovalidate, ruleset, context);
            if (!result.IsValid)
            {
                throw new ValidationException(result);
            }
        }
    }
}
