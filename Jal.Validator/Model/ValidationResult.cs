using System;
using System.Collections.Generic;
using System.Linq;

namespace Jal.Validator.Model
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            Errors = new List<ValidationFailure>();
            Warnings = new List<ValidationWarning>();
        }

        public ValidationResult(IList<ValidationFailure> failures)
        {
            Errors = failures;
            Warnings = new List<ValidationWarning>();
        }

        public IList<ValidationWarning> Warnings { get; set; }

        public IList<ValidationFailure> Errors
        {
            get;
            set;
        }

        public bool IsValid 
        {
            get { return Errors == null || Errors.Count == 0; }  
        }

        public List<string> ToList()
        {
            return Errors == null ? new List<string>() : (from s in Errors select s.ErrorMessage).ToList();
        }

        public override string ToString()
        {
            return Errors == null ? string.Empty : string.Join(Environment.NewLine,(from s in Errors select s.ErrorMessage).ToList());
        }
    }
}