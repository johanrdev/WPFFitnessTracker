using FitnessTracker.Domain.Models;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;

namespace FitnessTracker.Presentation.Module.Reports.ViewModels
{
    internal class ReportDetailViewModel : BindableBase, INavigationAware
    {
        private Report _report;
        private string _title;

        public Report Report
        {
            get => _report;
            set => SetProperty(ref _report, value);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            var report = navigationContext.Parameters.GetValue<Report>("Report");

            return Report.Id == report.Id ? true : false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.ContainsKey("Report"))
            {
                Report = navigationContext.Parameters.GetValue<Report>("Report");

                Title = Report.Date.ToString("MM/dd/yy");
            }
        }
    }
}
