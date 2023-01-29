using FitnessTracker.Application.Services;
using FitnessTracker.Domain.Events;
using FitnessTracker.Domain.Models;
using FitnessTracker.Presentation.Validation;
using FitnessTracker.Presentation.Validation.Rules;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Controls;

namespace FitnessTracker.Presentation.Module.Reports.ViewModels
{
    internal class ReportDetailViewModel : ValidationBindableBase, INavigationAware
    {
        private string _title;
        private int _id;
        private DateTime _date;
        private string _weight;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDataProvider<Report> _reportsProvider;

        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public DateTime Date
        {
            get => _date;
            set
            {
                var isValid = IsPropertyValid(value);

                if (isValid)
                {
                    SetProperty(ref _date, value);
                }
            }
        }

        public string Weight
        {
            get => _weight;
            set
            {
                var isValid = IsPropertyValid(value);

                if (isValid)
                {
                    SetProperty(ref _weight, value);
                }
            }
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public DelegateCommand UpdateCommand { get; }

        public ReportDetailViewModel(IEventAggregator eventAggregator, IDataProvider<Report> reportsProvider)
        {
            _eventAggregator = eventAggregator;
            _reportsProvider = reportsProvider;

            Errors = new Dictionary<string, IList<object>>();
            ValidationRules = new Dictionary<string, List<ValidationRule>>();
            ValidationRules.Add(nameof(Weight), new List<ValidationRule>() { new NumericValidationRules() });
            ValidationRules.Add(nameof(Date), new List<ValidationRule>() { new DateTimeValidationRules() });

            UpdateCommand = new DelegateCommand(ExecuteUpdateCommand);
        }

        private void ExecuteUpdateCommand()
        {
            var date = DateTime.Parse(string.Format("{0} 00:00:00", Date.Date.ToString("MM/dd/yyyy")), CultureInfo.GetCultureInfo("en-US"));
            var report = new Report { Id = Id, Date = date, Weight = Convert.ToDouble(Weight) };

            var result = _reportsProvider.Update(report);

            _reportsProvider.LoadData();

            _eventAggregator.GetEvent<ReloadReportsRepositoryEvent>().Publish();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            var report = navigationContext.Parameters.GetValue<Report>("Report");

            return Id == report.Id ? true : false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.ContainsKey("Report"))
            {
                var report = navigationContext.Parameters.GetValue<Report>("Report");

                Id = report.Id;
                Date = report.Date;
                Weight = report.Weight.ToString();

                Title = Date.ToString("MM/dd/yy");
            }
        }
    }
}
