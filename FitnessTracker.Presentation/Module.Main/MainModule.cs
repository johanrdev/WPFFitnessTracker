using FitnessTracker.Presentation.Constants;
using FitnessTracker.Presentation.Module.Main.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace FitnessTracker.Presentation.Module.Main
{
    internal class MainModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, ViewTypes.StartView);
            regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, ViewTypes.NavigationView);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<StartView>();
        }
    }
}
