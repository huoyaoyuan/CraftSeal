using CraftSeal.Pages;
using CraftSeal.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace CraftSeal;

public sealed partial class MainWindow : Window
{
    private readonly MainVM ViewModel = new()
    {
        RecentSessions =
        {
            new SessionVM { Title = "会话 1" },
            new SessionVM { Title = "会话 2" },
            new SessionVM { Title = "会话 3" },
        },
    };

    public MainWindow()
    {
        InitializeComponent();
    }

    private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.IsSettingsSelected)
        {
            RootFrame.Navigate(typeof(SettingsPage));
        }
        else if (args.SelectedItem is SessionVM session)
        {
            RootFrame.Navigate(typeof(SessionPage), session);
        }
        else if (args.SelectedItem is NavigationItemStub { PageType: Type p })
        {
            RootFrame.Navigate(p);
        }
    }
}
