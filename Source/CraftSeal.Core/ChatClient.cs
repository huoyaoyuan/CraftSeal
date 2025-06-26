using System.Net.Http.Json;
using System.Net.ServerSentEvents;
using System.Text.Json;
using CraftSeal.Api;

namespace CraftSeal;

public class ChatClient(
    string baseUrl,
    string bearerToken,
    HttpMessageHandler? httpMessageHandler = null)
    : IDisposable
{
    private readonly HttpClient _httpClient = new(httpMessageHandler ?? new HttpClientHandler())
    {
        BaseAddress = new Uri(baseUrl),
        DefaultRequestHeaders =
        {
            Authorization = new("Bearer", bearerToken),
        },
    };

    public void Dispose() => _httpClient.Dispose();

    public async Task<ChatResponse> CompleteAsync(IEnumerable<ChatRequestMessage> messages)
    {
        var request = new ChatRequest
        {
            Messages = messages,
            Model = "deepseek-reasoner",
            Stream = false,
        };

        using var response = await _httpClient.PostAsJsonAsync("v1/chat/completions", request, ChatApiSerializationContext.Default.ChatRequest).ConfigureAwait(false);
        var result = await response.EnsureSuccessStatusCode().Content
            .ReadFromJsonAsync(ChatApiSerializationContext.Default.ChatResponse)
            .ConfigureAwait(false);
        return result ?? throw new Exception("Top level null");
    }

    public async IAsyncEnumerable<ChatResponseChunk> ChunkedCompleteAsync(IEnumerable<ChatRequestMessage> messages)
    {
        var request = new ChatRequest
        {
            Messages = messages,
            Model = "deepseek-reasoner",
            Stream = true,
        };

        using var response = await _httpClient.PostAsJsonAsync("v1/chat/completions", request, ChatApiSerializationContext.Default.ChatRequest).ConfigureAwait(false);
        var responseStream = await response.EnsureSuccessStatusCode().Content.ReadAsStreamAsync().ConfigureAwait(false);

        var reader = SseParser.Create(responseStream, (name, value) => JsonSerializer.Deserialize(value, ChatApiSerializationContext.Default.ChatResponseChunk));
        await foreach (var item in reader.EnumerateAsync().ConfigureAwait(false))
            yield return item.Data ?? throw new Exception("Top level null");
    }
}
