using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CraftSeal.ViewModels;

internal partial class SessionVM : ObservableObject
{
    [ObservableProperty]
    private string _title = string.Empty;

    public ObservableCollection<MessageVM> Messages { get; } = [];

    [ObservableProperty]
    public string _messageText = string.Empty;

    private static bool CanEcho(string? message) => !string.IsNullOrEmpty(message);

    [RelayCommand(CanExecute = nameof(CanEcho))]
    private async Task EchoAsync(string? message)
    {
        ArgumentNullException.ThrowIfNull(message);

        MessageText = string.Empty;
        Messages.Add(new MessageVM { Role = MessageRole.User, Message = message });
        await Task.Delay(1000).ConfigureAwait(true);
        Messages.Add(new MessageVM { Role = MessageRole.Assistant, Message = message });
    }
}
