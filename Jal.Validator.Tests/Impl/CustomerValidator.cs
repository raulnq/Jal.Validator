using Jal.Validator.Impl;
using Jal.Validator.Model;
using Jal.Validator.Tests.Model;

namespace Jal.Validator.Tests.Impl
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public override ValidationResult Validate(Customer instance)
        {
            var result=new ValidationResult();

            if (string.IsNullOrWhiteSpace(instance.Name))
            {
                result.Errors.Add(new ValidationFailure("Name", "The Name should not be empty"));
            }

            if (instance.Age<18)
            {
                result.Errors.Add(new ValidationFailure("Age", "The Age should be greater than 18"));
            }

            return result;
        }

        public override ValidationResult Validate(Customer instance, string ruleSet)
        {
            var result = new ValidationResult();
            if (ruleSet == "GreaterThan21")
            {
                if (instance.Age < 21)
                {
                    result.Errors.Add(new ValidationFailure("Age", "The Age should be greater than 21", "1"));
                }
            }
            if (ruleSet == "LessThan10")
            {
                if (instance.Age > 10)
                {
                    result.Errors.Add(new ValidationFailure("Age", "The Age should be less than 10"));
                }
            }
            return result;
        }
    }
}