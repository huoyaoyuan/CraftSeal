using CommunityToolkit.Mvvm.ComponentModel;

namespace CraftSeal.ViewModels;

internal partial class SessionVM : ObservableObject
{
    [ObservableProperty]
    private string _title = string.Empty;
}
