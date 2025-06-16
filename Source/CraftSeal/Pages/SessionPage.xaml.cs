using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace CraftSeal.Pages;

/// <summary>
/// 会话根页面
/// </summary>
public sealed partial class SessionPage : Page
{
    public SessionPage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        DataContext = e.Parameter;
    }
}
