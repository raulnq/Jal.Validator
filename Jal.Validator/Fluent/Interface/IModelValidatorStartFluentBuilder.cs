using Jal.Factory.Interface;
using Jal.Validator.Interface;

namespace Jal.Validator.Fluent.Interface
{
    public interface IModelValidatorStartFluentBuilder
    {
        IModelValidatorFluentBuilder UseFactory(IValidatorFactory validatorFactory);

        IModelValidatorEndFluentBuilder UseModelValidator(IModelValidator modelValidator);

        IModelValidatorFluentBuilder UseFactory(IObjectFactory objectFactory);
    }
}