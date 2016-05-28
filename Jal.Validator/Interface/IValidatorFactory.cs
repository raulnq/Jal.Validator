namespace Jal.Validator.Interface
{
    public interface IValidatorFactory
    {
        IValidator<T>[] Create<T>(T instance, string validationgroup);
    }
}
