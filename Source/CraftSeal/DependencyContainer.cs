using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace CraftSeal;

public static class DependencyContainer
{
    public static readonly DependencyProperty DependencyContextProperty =
        DependencyProperty.RegisterAttached("DependencyContext", typeof(IServiceProvider), typeof(FrameworkElement), new PropertyMetadata(null));
    public static IServiceProvider? GetDependencyContext(FrameworkElement element) => (IServiceProvider?)element.GetValue(DependencyContextProperty);
    public static void SetDependencyContext(FrameworkElement element, IServiceProvider? value) => element.SetValue(DependencyContextProperty, value);

    public static IServiceProvider? RecursiveGetDependencyContext(FrameworkElement element)
    {
        DependencyObject? dependencyObject = element;
        while (dependencyObject != null)
        {
            if (dependencyObject is FrameworkElement e && e.GetValue(DependencyContextProperty) is IServiceProvider value)
                return value;
            dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
        }
        return null;
    }
}
