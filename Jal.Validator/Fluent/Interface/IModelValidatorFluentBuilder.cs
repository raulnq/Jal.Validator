using Jal.Validator.Interface;

namespace Jal.Validator.Fluent.Interface
{
    public interface IModelValidatorFluentBuilder : IModelValidatorEndFluentBuilder
    {
        IModelValidatorEndFluentBuilder UseInterceptor(IModelValidatorInterceptor modelValidatorInterceptor);
    }
}