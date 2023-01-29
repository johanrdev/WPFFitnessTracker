using FitnessTracker.Application.Services;
using FitnessTracker.Domain.Events;
using FitnessTracker.Domain.Models;
using FitnessTracker.Presentation.Constants;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace FitnessTracker.Presentation.Module.Reports.ViewModels
{
    internal class ReportsTabControlViewModel : BindableBase
    {
        private IRegionManager _regionManager;
        private IEventAggregator _eventAggregator;
        private IDataProvider<Report> _reportsProvider;

        public IDataProvider<Report> ReportsProvider
        {
            get => _reportsProvider;
            set => SetProperty(ref _reportsProvider, value);
        }

        public ReportsTabControlViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IDataProvider<Report> reportsProvider)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            ReportsProvider = reportsProvider;

            _eventAggregator.GetEvent<AfterLoadedReportsEvent>().Subscribe(Initialize);
            _eventAggregator.GetEvent<ReloadReportsRepositoryEvent>().Subscribe(Initialize);
            _eventAggregator.GetEvent<SelectReportChartPointEvent>().Subscribe(Navigate);
        }

        private void Initialize()
        {
            _regionManager.RequestNavigate(RegionNames.ReportsChartTabRegion, "ReportsChartView");

            _eventAggregator.GetEvent<LoadReportsTabControlEvent>().Publish();
        }

        private void Navigate(Report report)
        {
            var param = new NavigationParameters();

            param.Add("Report", report);

            _regionManager.RequestNavigate(RegionNames.ReportsChartTabRegion, "ReportDetailView", param);
        }
    }
}
