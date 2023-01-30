using FitnessTracker.Presentation.Constants;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Linq;

namespace FitnessTracker.Presentation.Module.Main.ViewModels
{
    internal class NavigationViewModel : BindableBase
    {
        private IRegionManager _regionManager;
        private NavigationItem _selectedNavigationItem;

        public NavigationItem SelectedNavigationItem
        {
            get => _selectedNavigationItem;
            set => SetProperty(ref _selectedNavigationItem, value);
        }

        public ObservableCollection<NavigationItem> NavigationItems { get; }
        public DelegateCommand<object> NavigateCommand { get; }

        public NavigationViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            NavigationItems = new ObservableCollection<NavigationItem>();
            NavigationItems.Add(
                new NavigationItem 
                { 
                    DisplayName = "Start", 
                    ViewName = ViewTypes.StartView.ToString(),
                    Icon = "Home"
                });
            NavigationItems.Add(
                new NavigationItem 
                { 
                    DisplayName = "Reports", 
                    ViewName = ViewTypes.ReportsView.ToString(),
                    Icon = "Database"
                });
            SelectedNavigationItem = NavigationItems.First();

            NavigateCommand = new DelegateCommand<object>(Execute);
        }

        private void Execute(object obj)
        {
            if (obj is NavigationItem)
            {
                var navigationItem = (NavigationItem)obj;

                _regionManager.RequestNavigate(RegionNames.ContentRegion, navigationItem.ViewName);
            }
        }

        public class NavigationItem
        {
            public string DisplayName { get; set; }
            public string ViewName { get; set; }
            public string Icon { get; set; }
        }
    }
}
