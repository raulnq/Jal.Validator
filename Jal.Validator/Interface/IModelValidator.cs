using Jal.Validator.Model;

namespace Jal.Validator.Interface
{
    public interface IModelValidator
    {
        ValidationResult Validate<T>(T instance);

        ValidationResult Validate<T>(T instance, string rulename);

        ValidationResult Validate<T>(T instance, string rulename, string ruleset);

        ValidationResult Validate<T>(T instance, dynamic context);

        ValidationResult Validate<T>(T instance, string rulename, dynamic context);

        ValidationResult Validate<T>(T instance, string rulename, string ruleset, dynamic context);
    }
}