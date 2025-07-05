using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CraftSeal.ViewModels;

internal partial class MainVM : ObservableObject
{
    public ObservableCollection<SessionVM> RecentSessions { get; } = [];

    [ObservableProperty]
    private SessionVM? _selectedSession;
}
