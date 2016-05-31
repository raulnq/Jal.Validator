using Jal.Validator.Interface;

namespace Jal.Validator.Fluent.Interface
{
    public interface IModelValidatorFluentBuilder : IModelValidatorEndFluentBuilder
    {
        IModelValidatorFluentBuilder UseInterceptor(IModelValidatorInterceptor modelValidatorInterceptor);
    }
}