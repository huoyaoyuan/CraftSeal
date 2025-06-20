using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CraftSeal.ViewModels;

internal partial class SessionVM : ObservableObject
{
    [ObservableProperty]
    private string _title = string.Empty;

    public ObservableCollection<MessageVM> Messages { get; } = [];

    public SessionVM()
    {
        SendCommand = new AsyncRelayCommand<string>(
            EchoAsync,
            canExecute: text => !string.IsNullOrEmpty(text));
    }

    [ObservableProperty]
    public string _messageText = string.Empty;

    public ICommand SendCommand { get; }

    private async Task EchoAsync(string? message)
    {
        ArgumentNullException.ThrowIfNull(message);

        MessageText = string.Empty;
        Messages.Add(new MessageVM { Role = MessageRole.User, Message = message });
        await Task.Delay(1000).ConfigureAwait(true);
        Messages.Add(new MessageVM { Role = MessageRole.Assistant, Message = message });
    }
}
