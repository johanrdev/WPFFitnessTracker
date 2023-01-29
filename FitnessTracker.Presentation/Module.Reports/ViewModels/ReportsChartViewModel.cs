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
        public List<Axis> YAxes { get; }
        public DelegateCommand<object> OpenChartPointDataCommand { get; }

        public ReportsChartViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IDataProvider<Report> reportsProvider)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _reportsProvider = reportsProvider;
            Series = new ObservableCollection<ISeries>();
            XAxes = new List<Axis>();
            YAxes = new List<Axis>();

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
            var yAxis = InitializeAxisY();

            Series.Clear();
            XAxes.Clear();
            YAxes.Clear();

            Series.Add(lineSeries);
            XAxes.Add(xAxis);
            YAxes.Add(yAxis);
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
                Stroke = new SolidColorPaint(new SKColor(194, 24, 91, 255)) { StrokeThickness = 4 },
                Fill = new SolidColorPaint(new SKColor(173, 20, 87, 127)),
                //GeometryFill = new SolidColorPaint(new SKColor(240, 98, 146, 255)),
                GeometryStroke = new SolidColorPaint(new SKColor(194, 24, 91, 255)) { StrokeThickness = 2 }
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
                LabelsRotation = 45,
                SeparatorsPaint = new SolidColorPaint(new SKColor(26, 35, 126)) { StrokeThickness = 2 },
                CrosshairPaint = new SolidColorPaint(new SKColor(40, 53, 147, 255)) { StrokeThickness = 2 },
                LabelsPaint = new SolidColorPaint(new SKColor(159, 168, 218, 255)),
                TextSize = 11.0
            };
        }

        private Axis InitializeAxisY()
        {
            return new Axis
            {
                SeparatorsPaint = new SolidColorPaint(new SKColor(26, 35, 126)) { StrokeThickness = 2 },
                LabelsPaint = new SolidColorPaint(new SKColor(159, 168, 218, 255)),
                TextSize = 11.0
            };
        }
    }
}
