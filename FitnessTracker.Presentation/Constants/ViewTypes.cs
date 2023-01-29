using FitnessTracker.Presentation.Module.Main.Views;
using FitnessTracker.Presentation.Module.Reports.Views;
using System;

namespace FitnessTracker.Presentation.Constants
{
    public static class ViewTypes
    {
        public static Type StartView = typeof(StartView);
        public static Type NavigationView = typeof(NavigationView);
        public static Type ReportsView = typeof(ReportsView);
        public static Type ReportsChartView = typeof(ReportsChartView);
        public static Type ReportsChartOptionsView = typeof(ReportsChartOptionsView);
        public static Type ReportsDataGridView = typeof(ReportsDataGridView);
        public static Type ReportDetailView = typeof(ReportDetailView);

        public static Type ReportsTabControlView = typeof(ReportsTabControlView);
    }
}
