using System.Diagnostics;
using System.Globalization;
using System.Windows.Controls;

namespace FitnessTracker.Presentation.Validation.Rules
{
    public class NumericValidationRules : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!decimal.TryParse(value.ToString(), out _))
            {
                Debug.WriteLine("Is not valid");
                return new ValidationResult(false, "Value must be a number with or without decimals.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
