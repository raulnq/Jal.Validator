using Jal.Validator.Model;

namespace Jal.Validator.Extension
{
    public static class StringExtension
    {
        public static ValidationResult ToValidationResult(this string message, string property = "", string code = "", string info = "")
        {
            var result = new ValidationResult();
            result.Errors.Add(message.ToValidationFailure(property, code, info));
            return result;
        }

        public static ValidationFailure ToValidationFailure(this string message, string property = "", string code="", string info="")
        {
            return new ValidationFailure(property, message, code, info);
        }
    }
}
