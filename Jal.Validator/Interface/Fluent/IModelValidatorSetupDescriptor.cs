namespace Jal.Validator.Interface.Fluent
{
    public interface IModelValidatorSetupDescriptor : IModelValidatorEndSetupDescriptor
    {
        IModelValidatorSetupDescriptor UseInterceptor(IModelValidatorInterceptor modelValidatorInterceptor);
    }
}