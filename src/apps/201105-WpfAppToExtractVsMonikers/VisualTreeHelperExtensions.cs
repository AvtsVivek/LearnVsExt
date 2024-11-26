using System.Windows.Media;
using System.Windows;

namespace WpfAppToExtractVsMonikers
{
    internal static class VisualTreeHelperExtensions
    {
        internal static T FindAncestor<T>(DependencyObject dependencyObject) where T : class
        {
            DependencyObject target = dependencyObject;

            do
            {
                target = VisualTreeHelper.GetParent(target);
            }
            while (target != null && !(target is T));

            return target as T;
        }
    }
}