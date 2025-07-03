using CraftSeal.Pages;
using CraftSeal.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

namespace CraftSeal;

public sealed partial class MainWindow : Window
{
    private readonly MainVM ViewModel = new()
    {
        RecentSessions =
        {
            new SessionVM
            {
                Title = "会话 1",
                Messages =
                {
                    new MessageVM { Role = MessageRole.User, Message = "Ping!" },
                    new MessageVM { Role = MessageRole.Assistant, Message = "Pong!" },
                },
            },
            new SessionVM { Title = "会话 2" },
            new SessionVM { Title = "会话 3" },
        },
    };

    public MainWindow(IServiceProvider serviceProvider)
    {
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
