using Jal.Validator.Model;

namespace Jal.Validator.Extension
{
    public static class StringExtension
    {
        public static ValidationResult ToValidationResult(this string message)
        {
            var result = new ValidationResult();
            result.Errors.Add(message.ToValidationFailure());
            return result;
        }

        /// <summary>
        /// {code}-{message}-{info}
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ValidationFailure ToValidationFailure(this string message)
        {
           var splitMessage = message.Split(ValidationSettings.TokenMessageSeparator);

           switch (splitMessage.Length)
            {
                case 1: return new ValidationFailure(string.Empty, splitMessage[0], string.Empty, string.Empty);
                case 2: return new ValidationFailure(string.Empty, splitMessage[1], splitMessage[0], string.Empty);
                case 3: return new ValidationFailure(string.Empty, splitMessage[1], splitMessage[0], splitMessage[2]);
               default: return new ValidationFailure(string.Empty, message, string.Empty, string.Empty);
            }
        }

        public static ValidationResult ToValidationResult(this string message, string property)
        {
            var result = new ValidationResult();
            result.Errors.Add(message.ToValidationFailure(property));
            return result;
        }

        /// <summary>
        /// {code}-{message}-{info}
        /// </summary>
        /// <param name="message"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static ValidationFailure ToValidationFailure(this string message, string property)
        {
            var splitMessage = message.Split(ValidationSettings.TokenMessageSeparator);

            switch (splitMessage.Length)
            {
                case 1: return new ValidationFailure(property, splitMessage[0], string.Empty, string.Empty);
                case 2: return new ValidationFailure(property, splitMessage[1], splitMessage[0], string.Empty);
                case 3: return new ValidationFailure(property, splitMessage[1], splitMessage[0], splitMessage[2]);
                default: return new ValidationFailure(property, message, string.Empty, string.Empty);
            }
        }
    }
}
