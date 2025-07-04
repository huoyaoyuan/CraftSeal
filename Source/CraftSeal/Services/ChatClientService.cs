using CraftSeal.Api;

namespace CraftSeal.Services;

internal class ChatClientService
{
    private ChatClient? _chatClient;

    public bool Initialized => _chatClient != null;

    public string ApiKey { get; private set; } = string.Empty;

    public void UpdateApiKey(string apiKey)
    {
        ApiKey = apiKey;
        _chatClient = new ChatClient("https://api.deepseek.com/", apiKey);
        // No disposal for old client, to keep old request alive
    }

    public IAsyncEnumerable<ChatResponseChunk> ChatAsync(IEnumerable<ChatRequestMessage> messages, CancellationToken cancellationToken = default)
    {
        if (_chatClient == null)
            throw new InvalidOperationException("Uninitialized!");
        return _chatClient.ChunkedCompleteAsync(messages, cancellationToken);
    }
}
