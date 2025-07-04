using CommunityToolkit.Mvvm.ComponentModel;

namespace CraftSeal.ViewModels;

public enum MessageRole
{
    User,
    Reasoning,
    Assistant,
}

internal partial class MessageVM : ObservableObject
{
    public MessageRole Role { get; set; }

    [ObservableProperty]
    private string _message = string.Empty;
}
