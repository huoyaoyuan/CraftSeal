using CommunityToolkit.Mvvm.ComponentModel;

namespace CraftSeal.ViewModels;

internal partial class SettingsVM : ObservableObject
{
    [ObservableProperty]
    private string _apiKey = string.Empty;
}
