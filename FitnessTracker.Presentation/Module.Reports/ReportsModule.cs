using FitnessTracker.Presentation.Constants;
using FitnessTracker.Presentation.Module.Reports.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace FitnessTracker.Presentation.Module.Reports
{
    internal class ReportsModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, ViewTypes.ReportsView);
            regionManager.RegisterViewWithRegion(RegionNames.ReportsChartRegion, ViewTypes.ReportsChartView);
            regionManager.RegisterViewWithRegion(RegionNames.ReportsChartOptionsRegion, ViewTypes.ReportsChartOptionsView);
            regionManager.RegisterViewWithRegion(RegionNames.ReportsDataGridRegion, ViewTypes.ReportsDataGridView);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ReportsView>();
        }
    }
}
