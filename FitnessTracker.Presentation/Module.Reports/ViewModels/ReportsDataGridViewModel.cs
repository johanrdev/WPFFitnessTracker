using FitnessTracker.Application.Services;
using FitnessTracker.Domain.Events;
using FitnessTracker.Domain.Models;
using FitnessTracker.Presentation.Module.Main.Dialogs;
using FitnessTracker.Presentation.Module.Reports.Dialogs;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Presentation.Module.Reports.ViewModels
{
    internal class ReportsDataGridViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;
        private IDialogService _dialogService;
        private IDataProvider<Report> _reportsProvider;

        public IDataProvider<Report> ReportsProvider
        {
            get => _reportsProvider;
            set => SetProperty(ref _reportsProvider, value);
        }

        public DelegateCommand AddReportCommand { get; }
        public DelegateCommand<object> DeleteSelectedCommand { get; }

        public ReportsDataGridViewModel(
            IEventAggregator eventAggregator, 
            IDialogService dialogService, 
            IDataProvider<Report> reportsProvider
            )
        {
            _eventAggregator = eventAggregator;
            _dialogService = dialogService;
            _reportsProvider = reportsProvider;

            LoadData();

            AddReportCommand = new DelegateCommand(ExecuteAddReportCommand);
            DeleteSelectedCommand = new DelegateCommand<object>(ExecuteDeleteSelectedCommand);
        }

        private void LoadData()
        {
            ReportsProvider.IsLoading = true;

            Task.Run(() =>
            {
                var reports = ReportsProvider.LoadData();

                App.Current.Dispatcher.Invoke(() =>
                {
                    ReportsProvider.SetData(reports);

                    ReportsProvider.IsLoading = false;

                    _eventAggregator.GetEvent<AfterLoadedReportsEvent>().Publish();
                });
            });
        }

        private void ExecuteAddReportCommand()
        {
            _dialogService.ShowDialog(nameof(AddReportDialog), null, callback => 
            {
                LoadData();
            });
        }

        private void ExecuteDeleteSelectedCommand(object obj)
        {
            _dialogService.ShowDialog(nameof(ConfirmActionDialog), null, callback =>
            {
                if (callback.Result == ButtonResult.OK)
                {
                    var list = ((IList)obj).OfType<Report>().ToList();

                    var result = ReportsProvider.RemoveRange(list);

                    LoadData();
                }
            });
        }
    }
}
