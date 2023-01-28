using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;

namespace FitnessTracker.Presentation.Module.Main.Dialogs
{
    internal class ConfirmActionDialogViewModel : BindableBase, IDialogAware
    {
        private string _message;
        public string Title => "Confirm Action";

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public DelegateCommand ConfirmCommand { get; }
        public DelegateCommand CancelCommand { get; }

        public event Action<IDialogResult> RequestClose;

        public ConfirmActionDialogViewModel()
        {
            ConfirmCommand = new DelegateCommand(ExecuteConfirmCommand);
            CancelCommand = new DelegateCommand(ExecuteCancelCommand);
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {

        }

        private void ExecuteConfirmCommand()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
        }

        private void ExecuteCancelCommand()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
        }
    }
}
