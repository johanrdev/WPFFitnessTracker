using FitnessTracker.Application.Services;
using FitnessTracker.Domain.Events;
using FitnessTracker.Domain.Models;
using LiveChartsCore;
using LiveChartsCore.Kernel;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FitnessTracker.Presentation.Module.Reports.ViewModels
{
    internal class ReportsChartViewModel : BindableBase
    {
        private IRegionManager _regionManager;
        private IEventAggregator _eventAggregator;
        private IDataProvider<Report> _reportsProvider;

        public IDataProvider<Report> ReportsProvider
        {
            get => _reportsProvider;
            set => SetProperty(ref _reportsProvider, value);
        }

        public string Title => "Diagram";
        public bool IsClosable => false;

        public ObservableCollection<ISeries> Series { get; }
        public List<Axis> XAxes { get; }
        public DelegateCommand<object> OpenChartPointDataCommand { get; }

        public ReportsChartViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IDataProvider<Report> reportsProvider)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _reportsProvider = reportsProvider;
            Series = new ObservableCollection<ISeries>();
            XAxes = new List<Axis>();

            eventAggregator.GetEvent<LoadReportsTabControlEvent>().Subscribe(Initialize);
            eventAggregator.GetEvent<ReloadReportsRepositoryEvent>().Subscribe(Initialize);

            OpenChartPointDataCommand = new DelegateCommand<object>(ExecuteOpenChartPointDataCommand);

        }

        private void ExecuteOpenChartPointDataCommand(object obj)
        {
            if (obj == null) return;

            if (obj is ChartPoint)
            {
                var point = (ChartPoint)obj;
                var report = (Report)point.Context.DataSource;

                _eventAggregator.GetEvent<SelectReportChartPointEvent>().Publish(report);
            }
        }

        private void Initialize()
        {
            if (ReportsProvider.Data == null) return;

            var lineSeries = InitializeLineSeries();
            var xAxis = InitializeAxisX();

            Series.Clear();
            XAxes.Clear();

            Series.Add(lineSeries);
            XAxes.Add(xAxis);
        }

        private LineSeries<Report> InitializeLineSeries()
        {
            return new LineSeries<Report>
            {
                Mapping = (x, y) =>
                {
                    y.PrimaryValue = x.Weight;
                    y.SecondaryValue = x.Date.Ticks;
                    y.TertiaryValue = x.Id;
                },
                Name = "Weight",
                Values = ReportsProvider.Data,
                TooltipLabelFormatter = value => $"{value.Context.Series.Name}: {value.PrimaryValue} KG",
                LineSmoothness = 0,
                GeometrySize = 10,
                Stroke = new SolidColorPaint(new SKColor(0, 137, 123, 255)) { StrokeThickness = 4 },
                Fill = new SolidColorPaint(new SKColor(0, 137, 123, 50)),
                GeometryFill = new SolidColorPaint(new SKColor(0, 137, 123, 255)),
                GeometryStroke = new SolidColorPaint(new SKColor(0, 137, 123, 255))
            };
        }

        private Axis InitializeAxisX()
        {
            return new Axis
            {
                Labeler = value => value < 0.0
                    ? (new DateTime((long)0.0).ToString("MM/dd/yy"))
                    : (new DateTime((long)value).ToString("MM/dd/yy")),
                UnitWidth = TimeSpan.FromDays(1).Ticks,
                MinStep = TimeSpan.FromDays(1).Ticks,
                LabelsRotation = 45
            };
        }
    }
}
