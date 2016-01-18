using Jal.Validator.Model;

namespace Jal.Validator.Interface
{
    public interface IValidator<in TTarget> : IValidator
    {
        ValidationResult Validate(TTarget instance);

        ValidationResult Validate(TTarget instance, dynamic context);

        ValidationResult Validate(TTarget instance, string subgroup);

        ValidationResult Validate(TTarget instance, string subgroup, dynamic context);
    }

    public interface IValidator
    {
        
    }
}