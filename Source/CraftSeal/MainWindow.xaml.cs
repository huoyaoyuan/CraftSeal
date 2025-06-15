using CraftSeal.ViewModels;
using Microsoft.UI.Xaml;

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
}
