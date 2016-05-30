namespace Jal.Validator.Interface.Fluent
{
    public interface IModelValidatorFluentBuilder : IModelValidatorEndFluentBuilder
    {
        IModelValidatorFluentBuilder UseInterceptor(IModelValidatorInterceptor modelValidatorInterceptor);
    }
}