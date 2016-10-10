using Jal.Factory.Interface;

namespace Jal.Validator.Fluent.Interface
{
    public interface IModelValidatorStartFluentBuilder
    {
        IModelValidatorFluentBuilder UseFactory(IObjectFactory objectFactory);
    }
}