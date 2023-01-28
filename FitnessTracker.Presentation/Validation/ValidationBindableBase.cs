using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace FitnessTracker.Presentation.Validation
{
    public class ValidationBindableBase : BindableBase, INotifyDataErrorInfo
    {
        protected Dictionary<string, IList<object>> Errors { get; set; }
        protected Dictionary<string, List<ValidationRule>> ValidationRules { get; set; }
        public bool HasErrors => Errors.Any();

        public bool IsPropertyValid<TValue>(TValue propertyValue, [CallerMemberName] string propertyName = null)
        {
            _ = ClearErrors(propertyName);

            if (ValidationRules.TryGetValue(propertyName, out List<ValidationRule> propertyValidationRules))
            {
                var errorMessage = propertyValidationRules
                    .Select(validationRule => validationRule.Validate(propertyValue, CultureInfo.CurrentCulture))
                    .Where(result => !result.IsValid)
                    .Select(invalidResult => invalidResult.ErrorContent);

                AddErrorRange(propertyName, errorMessage);

                return !errorMessage.Any();
            }

            return true;
        }

        private void AddErrorRange(string propertyName, IEnumerable<object> newErrors, bool isWarning = false)
        {
            if (!newErrors.Any()) return;

            if (!Errors.TryGetValue(propertyName, out IList<object> propertyErrors))
            {
                propertyErrors = new List<object>();

                Errors.Add(propertyName, propertyErrors);
            }

            if (isWarning)
            {
                foreach (var error in newErrors) propertyErrors.Add(error);
            }
            else
            {
                foreach (var error in newErrors) propertyErrors.Insert(0, error);
            }

            OnErrorsChanged(propertyName);
        }

        public bool ClearErrors(string propertyName)
        {
            if (Errors.Remove(propertyName))
            {
                OnErrorsChanged(propertyName);

                return true;
            }

            return false;
        }

        public bool PropertyHasErrors(string propertyName) => Errors.TryGetValue(propertyName, out IList<object> propertyErrors) && propertyErrors.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public System.Collections.IEnumerable GetErrors(string propertyName) => string.IsNullOrWhiteSpace(propertyName)
            ? Errors.SelectMany(entry => entry.Value)
            : Errors.TryGetValue(propertyName, out IList<object> errors)
                ? (IEnumerable<object>)errors
                : new List<object>();

        protected virtual void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
