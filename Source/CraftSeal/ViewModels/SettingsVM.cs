using CommunityToolkit.Mvvm.ComponentModel;
using CraftSeal.Services;

namespace CraftSeal.ViewModels;

internal partial class SettingsVM(ChatClientService chatClient) : ObservableObject
{
    [ObservableProperty]
    private string _apiKey = chatClient.ApiKey;

    partial void OnApiKeyChanged(string value) => chatClient.UpdateApiKey(value);
}
