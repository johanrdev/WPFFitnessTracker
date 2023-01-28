using FitnessTracker.Application.Services;
using FitnessTracker.Domain.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Diagnostics;

namespace FitnessTracker.Presentation.Module.Reports.Dialogs
{
    public class AddReportDialogViewModel : BindableBase, IDialogAware
    {
        private Report _newReport;
        private IDataProvider<Report> _reportsProvider;

        public string Title => "Add Report";

        public Report NewReport
        {
            get => _newReport;
            set => SetProperty(ref _newReport, value);
        }

        public IDataProvider<Report> ReportsProvider
        {
            get => _reportsProvider;
            set => SetProperty(ref _reportsProvider, value);
        }

        public DelegateCommand AddCommand { get; }
        public DelegateCommand CancelCommand { get; }

        public event Action<IDialogResult> RequestClose;

        public AddReportDialogViewModel(IDataProvider<Report> reportsProvider)
        {
            _reportsProvider = reportsProvider;

            AddCommand = new DelegateCommand(ExecuteAddCommand);
            CancelCommand = new DelegateCommand(ExecuteCancelCommand);
        }

        private void ExecuteAddCommand()
        {
            var result = ReportsProvider.Add(NewReport);

            if (result == 1)
            {
                Debug.WriteLine("Added!");
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
            } 
            else
            {
                Debug.WriteLine("Something went wrong");
            }
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
            NewReport = new Report();
        }
    }
}
