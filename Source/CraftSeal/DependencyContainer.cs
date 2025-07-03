using Microsoft.UI.Xaml;

namespace CraftSeal;

public static class DependencyContainer
{
    public static readonly DependencyProperty DependencyContextProperty =
        DependencyProperty.RegisterAttached("DependencyContext", typeof(IServiceProvider), typeof(FrameworkElement), new PropertyMetadata(null));
    public static IServiceProvider? GetDependencyContext(FrameworkElement element) => (IServiceProvider?)element.GetValue(DependencyContextProperty);
    public static void SetDependencyContext(FrameworkElement element, IServiceProvider? value) => element.SetValue(DependencyContextProperty, value);
}
