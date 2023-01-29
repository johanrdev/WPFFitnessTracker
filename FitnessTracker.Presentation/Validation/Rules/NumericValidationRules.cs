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
                return new ValidationResult(false, "Value must be a number.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
