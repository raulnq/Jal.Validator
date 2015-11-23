namespace Jal.Validator.Model
{
    public class ValidationFailure
    {
        public ValidationFailure(string errorMessage)
            : this(string.Empty, errorMessage)
        {

        }

        public ValidationFailure(string propertyName, string errorMessage)
            : this(propertyName, errorMessage, string.Empty)
        {

        }

        public ValidationFailure(string propertyName, string errorMessage, string errorCode)
            : this(propertyName, errorMessage, errorCode, string.Empty)
        {

        }

        public ValidationFailure(string propertyName, string errorMessage, string errorCode, string errorInfo)
        {
            ErrorMessage = errorMessage;

            ErrorCode = errorCode;

            PropertyName = propertyName;

            ErrorInfo = errorInfo;
        }

        public string ErrorMessage { get; set; }

        public string ErrorInfo { get; set; }

        public string PropertyName { get; set; }

        public string ErrorCode { get; set; }

    }
}