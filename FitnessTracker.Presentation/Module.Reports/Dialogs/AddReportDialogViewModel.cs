using FitnessTracker.Application.Services;
using FitnessTracker.Domain.Models;
using FitnessTracker.Presentation.Validation;
using FitnessTracker.Presentation.Validation.Rules;
using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Controls;

namespace FitnessTracker.Presentation.Module.Reports.Dialogs
{
    public class AddReportDialogViewModel : ValidationBindableBase, IDialogAware
    {
        private DateTime _newDate;
        private string _newWeight;

        public string Title => "Add Report";

        public DateTime NewDate
        {
            get => _newDate;
            set {
                bool isValid = IsPropertyValid(value);

                if (isValid)
                {
                    //_newDate = value;

                    SetProperty(ref _newDate, value);
                }
            }
        }

        public string NewWeight
        {
            get => _newWeight;
            set
            {
                bool isValid = IsPropertyValid(value);

                if (isValid)
                {
                    //_newWeight = value;

                    SetProperty(ref _newWeight, value);
                }
            }
        }

        public DelegateCommand AddCommand { get; }
        public DelegateCommand CancelCommand { get; }

        public event Action<IDialogResult> RequestClose;

        public AddReportDialogViewModel()
        {
            Errors = new Dictionary<string, IList<object>>();
            ValidationRules = new Dictionary<string, List<ValidationRule>>();
            ValidationRules.Add(nameof(NewWeight), new List<ValidationRule>() { new NumericValidationRules() });
            ValidationRules.Add(nameof(NewDate), new List<ValidationRule>() { new DateTimeValidationRules() });

            AddCommand = new DelegateCommand(ExecuteAddCommand);
            CancelCommand = new DelegateCommand(ExecuteCancelCommand);
        }

        private void ExecuteAddCommand()
        {
            var newDate = DateTime.Parse(string.Format("{0} 00:00:00", NewDate.Date.ToString("MM/dd/yyyy")), CultureInfo.GetCultureInfo("en-US"));
            var newReport = new Report { Date = newDate, Weight = Convert.ToDouble(NewWeight) };
            var dialogParameters = new DialogParameters();

            dialogParameters.Add("NewReport", newReport);

            RequestClose?.Invoke(new DialogResult(ButtonResult.OK, dialogParameters));
        }

        private void ExecuteCancelCommand()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            NewDate = DateTime.Now;
            NewWeight = "80.0";
        }
    }
}
