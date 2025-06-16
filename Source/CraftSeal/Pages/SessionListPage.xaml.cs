using CraftSeal.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

namespace CraftSeal.Pages;

/// <summary>
/// 会话列表页
/// </summary>
public sealed partial class SessionListPage : Page
{
    public SessionListPage()
    {
        InitializeComponent();
    }

    private void ListView_ItemClick(object sender, ItemClickEventArgs e)
    {
        var selected = (SessionVM)e.ClickedItem;
        ((MainVM)DataContext).SelectedSession = selected;
        Frame.Navigate(typeof(SessionPage), selected, new DrillInNavigationTransitionInfo());
    }
}
