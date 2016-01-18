using Jal.Validator.Model;

namespace Jal.Validator.Interface
{
    public interface IModelValidator
    {
        ValidationResult Validate<T>(T instance);

        ValidationResult Validate<T>(T instance, string @group);

        ValidationResult Validate<T>(T instance, string @group, string subgroup);

        ValidationResult Validate<T>(T instance, dynamic context);

        ValidationResult Validate<T>(T instance, string @group, dynamic context);

        ValidationResult Validate<T>(T instance, string @group, string subgroup, dynamic context);
    }
}