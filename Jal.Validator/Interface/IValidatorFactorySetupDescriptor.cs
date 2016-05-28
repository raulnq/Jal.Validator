using Jal.Factory.Interface;

namespace Jal.Validator.Interface
{
    public interface IValidatorFactorySetupDescriptor
    {
        IModelValidatorSetupDescriptor UseValidatorFactory(IValidatorFactory validatorFactory);
    }
}