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
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ReportsView>();
        }
    }
}
