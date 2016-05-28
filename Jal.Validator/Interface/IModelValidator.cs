using Jal.Validator.Model;

namespace Jal.Validator.Interface
{
    public interface IModelValidator
    {
        ValidationResult Validate<T>(T instance);

        ValidationResult Validate<T>(T instance, string validationgroup);

        ValidationResult Validate<T>(T instance, string validationgroup, string validationsubgroup);

        ValidationResult Validate<T>(T instance, dynamic context);

        ValidationResult Validate<T>(T instance, string validationgroup, dynamic context);

        ValidationResult Validate<T>(T instance, string validationgroup, string validationsubgroup, dynamic context);

        IValidatorFactory Factory { get; }
    }
}