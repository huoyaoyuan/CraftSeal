using CommunityToolkit.Mvvm.ComponentModel;

namespace CraftSeal.ViewModels;

public enum MessageRole
{
    User,
    Assistant,
}

internal partial class MessageVM : ObservableObject
{
    public MessageRole Role { get; set; }

    [ObservableProperty]
    private string _message = string.Empty;
}
