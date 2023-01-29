using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Controls;

namespace FitnessTracker.Presentation.Validation.Rules
{
    public class DateTimeValidationRules : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!DateTime.TryParse(value.ToString(), out _))
            {
                return new ValidationResult(false, "Value must be a date.");
            }

            if (DateTime.Parse(value.ToString()) > DateTime.Now)
            {
                Debug.WriteLine(value);

                return new ValidationResult(false, "Value cannot be a future date.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
