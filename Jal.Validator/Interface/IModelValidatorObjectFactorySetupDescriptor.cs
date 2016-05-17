using Jal.Factory.Interface;

namespace Jal.Validator.Interface
{
    public interface IModelValidatorObjectFactorySetupDescriptor
    {
        IModelValidatorSetupDescriptor UseObjectFactory(IObjectFactory objectFactory);
    }
}