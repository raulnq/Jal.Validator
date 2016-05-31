using Jal.Validator.Interface;

namespace Jal.Validator.Fluent.Interface
{
    public interface IModelValidatorEndFluentBuilder
    {
        IModelValidator Create { get; }
    }
}