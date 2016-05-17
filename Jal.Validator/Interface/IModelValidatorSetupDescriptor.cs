namespace Jal.Validator.Interface
{
    public interface IModelValidatorSetupDescriptor
    {
        IModelValidatorSetupDescriptor UseModelValidator(IModelValidator modelValidator);

        IModelValidator Create();
    }
}