using Jal.Validator.Interface;

namespace Jal.Validator.Impl
{
    public abstract class AbstractContextValidator<TTarget> : AbstractValidator<TTarget>, IValidatorContextContainer
    {
        public dynamic Context 
        { 
            get;
            set;
        }
    }
}
