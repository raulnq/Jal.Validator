using Jal.Factory.Interface;

namespace Jal.Validator.Interface.Fluent
{
    public interface IModelValidatorStartSetupDescriptor
    {
        IModelValidatorSetupDescriptor UseFactory(IValidatorFactory validatorFactory);

        IModelValidatorEndSetupDescriptor UseModelValidator(IModelValidator modelValidator);

        IModelValidatorSetupDescriptor UseObjectFactory(IObjectFactory objectFactory);
    }
}