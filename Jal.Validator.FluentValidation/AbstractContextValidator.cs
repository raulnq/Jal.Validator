using Jal.Validator.Interface;

namespace Jal.Validator.FluentValidation
{
    public abstract class AbstractContextValidator<TTarget> : FluentAbstractValidator<TTarget>, IValidatorContextContainer
    {
        public dynamic Context
        {
            get;
            set;
        }
    }
}
