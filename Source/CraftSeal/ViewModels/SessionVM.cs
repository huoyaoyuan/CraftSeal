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

        MessageVM? reasoningVM = null;
        MessageVM? resultVM = null;

        var response = _chatClient.ChatAsync(
            [
                new SystemMessage { Content = "你是一个人工智能助手，请回答用户的问题。" },
                .. Messages.Select(m => m.Role switch
                {
                    MessageRole.User => (ChatRequestMessage)new UserMessage { Content = m.Message },
                    MessageRole.Assistant => new AssistantMessage { Content = m.Message },
                    _ => null!,
                }).Where(x => x != null),
                new UserMessage { Content = message },
            ]);

        Messages.Add(new MessageVM { Role = MessageRole.User, Message = message });

        await foreach (var chunk in response.ConfigureAwait(true))
        {
            var delta = chunk.Choices[0].Delta;

            if (delta.ReasoningContent != null)
            {
                if (reasoningVM is null)
                {
                    reasoningVM = new() { Role = MessageRole.Reasoning };
                    Messages.Add(reasoningVM);
                }
                reasoningVM.Message += delta.ReasoningContent;
            }

            if (delta.Content != null)
            {
                if (resultVM is null)
                {
                    resultVM = new() { Role = MessageRole.Assistant };
                    Messages.Add(resultVM);
                }
                resultVM.Message += delta.Content;
            }
        }
    }
}
