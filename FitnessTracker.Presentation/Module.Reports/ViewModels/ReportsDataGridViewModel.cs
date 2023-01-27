using FitnessTracker.Application.Services;
using FitnessTracker.Domain.Events;
using FitnessTracker.Domain.Models;
using Prism.Events;
using Prism.Mvvm;
using System.Threading.Tasks;

namespace FitnessTracker.Presentation.Module.Reports.ViewModels
{
    internal class ReportsDataGridViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;
        private IDataProvider<Report> _reportsProvider;

        public IDataProvider<Report> ReportsProvider
        {
            get => _reportsProvider;
            set => SetProperty(ref _reportsProvider, value);
        }

        public ReportsDataGridViewModel(IEventAggregator eventAggregator, IDataProvider<Report> reportsProvider)
        {
            _eventAggregator = eventAggregator;
            _reportsProvider = reportsProvider;

            LoadData();
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
    }
}
