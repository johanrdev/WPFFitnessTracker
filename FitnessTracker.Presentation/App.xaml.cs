using FitnessTracker.Application.Services;
using FitnessTracker.Domain.Models;
using FitnessTracker.Infrastructure.Services;
using FitnessTracker.Presentation.Module.Main;
using FitnessTracker.Presentation.Module.Main.Views;
using FitnessTracker.Presentation.Module.Reports;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;

namespace FitnessTracker.Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<ShellWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register services
            containerRegistry.RegisterSingleton<IDataProvider<Report>, ReportsProvider>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<MainModule>();
            moduleCatalog.AddModule<ReportsModule>();
        }
    }
}
