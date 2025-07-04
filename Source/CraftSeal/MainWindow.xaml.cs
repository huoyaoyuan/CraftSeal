using CraftSeal.Pages;
using CraftSeal.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

namespace CraftSeal;

public sealed partial class MainWindow : Window
{
    private readonly MainVM ViewModel;

    public MainWindow(IServiceProvider serviceProvider)
    {
        ViewModel = new()
        {
            RecentSessions =
            {
                new SessionVM(serviceProvider) { Title = "会话 1" },
                new SessionVM(serviceProvider) { Title = "会话 2" },
                new SessionVM(serviceProvider) { Title = "会话 3" },
            },
        };
        InitializeComponent();
        DependencyContainer.SetDependencyContext((FrameworkElement)Content, serviceProvider);
    }

    private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.IsSettingsSelected)
        {
            RootFrame.Navigate(typeof(SettingsPage), null, new EntranceNavigationTransitionInfo());
        }
        else if (args.SelectedItem is SessionVM session)
        {
            RootFrame.Navigate(typeof(SessionPage), session, new EntranceNavigationTransitionInfo());
        }
        else if (args.SelectedItem is NavigationItemStub { PageType: Type p })
        {
            RootFrame.Navigate(p, null, new EntranceNavigationTransitionInfo());
        }
    }
}
