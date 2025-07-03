using CraftSeal.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace CraftSeal.Pages;

/// <summary>
/// 设置页
/// </summary>
public sealed partial class SettingsPage : Page
{
    public SettingsPage()
    {
        DataContext = null;
        Loaded += SettingsPage_Loaded;
        InitializeComponent();
    }

    private void SettingsPage_Loaded(object sender, RoutedEventArgs e)
    {
        var sp = DependencyContainer.RecursiveGetDependencyContext(this);
        DataContext = sp?.GetService<SettingsVM>();
    }
}
