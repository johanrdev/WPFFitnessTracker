﻿using Microsoft.Xaml.Behaviors;
using Prism.Regions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FitnessTracker.Presentation.Actions
{
    public class CloseTabTriggerAction : TriggerAction<Button>
    {
        protected override void Invoke(object parameter)
        {
            var args = parameter as RoutedEventArgs;

            if (args == null) return;

            var tabItem = FindParent<TabItem>(args.OriginalSource as DependencyObject);

            if (tabItem == null) return;

            var tabControl = FindParent<TabControl>(tabItem);

            if (tabControl == null) return;

            IRegion region = RegionManager.GetObservableRegion(tabControl).Value;

            if (region == null) return;

            if (region.Views.Contains(tabItem.Content)) region.Remove(tabItem.Content);
        }

        static T FindParent<T>(DependencyObject child) where T : DependencyObject {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) return null;

            var parent = parentObject as T;
            if (parent != null) return parent;

            return FindParent<T>(parentObject);
        }
    }
}
