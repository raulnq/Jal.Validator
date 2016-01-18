using Jal.Validator.Interface;

namespace Jal.Validator.Impl
{
    public abstract class AbstractTransientValidator<TTarget> : AbstractValidator<TTarget>, ITransientValidator
    {
        public dynamic Context { get;set; }

        public string Group { get; set; }

        public string Subgroup { get; set; }
    }
}
