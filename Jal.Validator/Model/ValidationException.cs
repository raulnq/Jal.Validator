using System;

namespace Jal.Validator.Model
{
    public class ValidationException : Exception
    {
        public ValidationResult ValidationResult { get; set; }

        public ValidationException(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }   

        public ValidationException(string message)
            : base(message)
        {
        }

        public ValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
