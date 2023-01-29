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
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Presentation.Module.Reports.ViewModels
{
    internal class ReportsDataGridViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;
        private IDialogService _dialogService;
        private IDataProvider<Report> _reportsProvider;
        private Report _selectedReport;

        public IDataProvider<Report> ReportsProvider
        {
            get => _reportsProvider;
            set => SetProperty(ref _reportsProvider, value);
        }

        public Report SelectedReport
        {
            get => _selectedReport;
            set => SetProperty(ref _selectedReport, value);
        }

        public DelegateCommand AddReportCommand { get; }
        public DelegateCommand<object> DeleteSelectedCommand { get; }
        public DelegateCommand<object> OpenReportDetailCommand { get; }

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
            DeleteSelectedCommand = new DelegateCommand<object>(ExecuteDeleteSelectedCommand, CanExecuteDeleteSelectedCommand)
                .ObservesProperty(() => SelectedReport);
            OpenReportDetailCommand = new DelegateCommand<object>(ExecuteOpenReportDetailCommand);

            _eventAggregator.GetEvent<ReloadReportsRepositoryEvent>().Subscribe(LoadData);
        }

        private void ExecuteOpenReportDetailCommand(object obj)
        {
            if (obj == null) return;

            if (obj is Report)
            {
                var report = (Report)obj;

                _eventAggregator.GetEvent<SelectReportChartPointEvent>().Publish(report);
            }
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
                if (callback.Result == ButtonResult.OK)
                {
                    var containsNewReport = callback.Parameters.ContainsKey("NewReport");

                    if (containsNewReport)
                    {
                        var newReport = callback.Parameters.GetValue<Report>("NewReport");

                        ReportsProvider.Add(newReport);

                        LoadData();
                    }
                }
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

        private bool CanExecuteDeleteSelectedCommand(object arg)
        {
            return SelectedReport != null;
        }
    }
}
