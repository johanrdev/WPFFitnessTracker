using Prism.Services.Dialogs;
using System;

namespace FitnessTracker.Presentation.Module.Reports.Dialogs
{
    public class AddReportDialogViewModel : IDialogAware
    {
        public string Title => "Add Report";

        public event Action<IDialogResult> RequestClose;

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

        }
    }
}
