using FitnessTracker.Presentation.Constants;
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
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
