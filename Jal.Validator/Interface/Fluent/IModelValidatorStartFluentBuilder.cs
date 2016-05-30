using Jal.Factory.Interface;

namespace Jal.Validator.Interface.Fluent
{
    public interface IModelValidatorStartFluentBuilder
    {
        IModelValidatorFluentBuilder UseFactory(IValidatorFactory validatorFactory);

        IModelValidatorEndFluentBuilder UseModelValidator(IModelValidator modelValidator);

        IModelValidatorFluentBuilder UseObjectFactory(IObjectFactory objectFactory);
    }
}