using Jal.Validator.Interface;
using Jal.Validator.Model;

namespace Jal.Validator.Impl
{
    public abstract class AbstractValidator<TTarget> : IValidator<TTarget>
    {
        public virtual ValidationResult Validate(TTarget instance)
        {
            return new ValidationResult();
        }

        public virtual ValidationResult Validate(TTarget instance, dynamic context)
        {
            return Validate(instance);
        }

        public virtual ValidationResult Validate(TTarget instance, string subgroup)
        {
            return Validate(instance);
        }

        public virtual ValidationResult Validate(TTarget instance, string subgroup, dynamic context)
        {
            return Validate(instance, context);
        }
    }
}
