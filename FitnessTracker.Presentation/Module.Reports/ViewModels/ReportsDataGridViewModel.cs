using FitnessTracker.Application.Services;
using FitnessTracker.Domain.Models;
using Prism.Mvvm;
using System.Collections;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FitnessTracker.Presentation.Module.Reports.ViewModels
{
    internal class ReportsDataGridViewModel : BindableBase
    {
        private IDataProvider<Report> _reportsProvider;

        public IDataProvider<Report> ReportsProvider
        {
            get => _reportsProvider;
            set => SetProperty(ref _reportsProvider, value);
        }

        public ReportsDataGridViewModel(IDataProvider<Report> reportsProvider)
        {
            _reportsProvider = reportsProvider;

            LoadData();
        }

        private void LoadData()
        {
            Task.Run(() =>
            {
                var reports = ReportsProvider.LoadData();

                App.Current.Dispatcher.Invoke(() => ReportsProvider.SetData(reports));
            });
        }
    }
}
