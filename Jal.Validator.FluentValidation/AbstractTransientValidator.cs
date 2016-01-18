using Jal.Validator.Interface;

namespace Jal.Validator.FluentValidation
{
    public abstract class AbstractTransientValidator<TTarget> : FluentAbstractValidator<TTarget>, ITransientValidator
    {
        public dynamic Context { get;set; }

        public string Group { get; set; }

        public string Subgroup { get; set; }
    }
}
