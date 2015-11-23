namespace Jal.Validator.Model
{
    public class ValidationWarning
    {
        public ValidationWarning(string warningMessage)
        {
            WarningMessage = warningMessage;
        }

        public ValidationWarning(string propertyName, string warningMessage)
        {
            WarningMessage = warningMessage;

            PropertyName = propertyName;
        }

        public string WarningMessage { get; set; }

        public string PropertyName { get; set; }
    }
}