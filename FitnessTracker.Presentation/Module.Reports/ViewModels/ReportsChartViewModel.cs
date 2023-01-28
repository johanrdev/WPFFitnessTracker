using FitnessTracker.Application.Services;
using FitnessTracker.Domain.Events;
using FitnessTracker.Domain.Models;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FitnessTracker.Presentation.Module.Reports.ViewModels
{
    internal class ReportsChartViewModel : BindableBase
    {
        private IDataProvider<Report> _reportsProvider;

        public IDataProvider<Report> ReportsProvider
        {
            get => _reportsProvider;
            set => SetProperty(ref _reportsProvider, value);
        }

        public ObservableCollection<ISeries> Series { get; }
        public List<Axis> XAxes { get; }
        public DelegateCommand ChartPointPointerDownCommand { get; }

        public ReportsChartViewModel(IEventAggregator eventAggregator, IDataProvider<Report> reportsProvider)
        {
            _reportsProvider = reportsProvider;
            Series = new ObservableCollection<ISeries>();
            XAxes = new List<Axis>();

            eventAggregator.GetEvent<AfterLoadedReportsEvent>().Subscribe(Initialize);
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
