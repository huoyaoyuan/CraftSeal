using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CraftSeal.Api;
using CraftSeal.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CraftSeal.ViewModels;

internal partial class SessionVM(IServiceProvider serviceProvider) : ObservableObject
{
    private readonly ChatClientService _chatClient = serviceProvider.GetRequiredService<ChatClientService>();

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

        var response = await _chatClient.ChatAsync(
            [
                new SystemMessage { Content = "你是一个复读机，请复述你收到的消息" },
                new UserMessage { Content = message },
            ]).ConfigureAwait(true);

        Messages.Add(new MessageVM { Role = MessageRole.Assistant, Message = response.Choices[0].Message.Content ?? string.Empty });
    }
}
