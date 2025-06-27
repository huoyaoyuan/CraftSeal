using System.Net.Http.Json;
using System.Net.ServerSentEvents;
using System.Runtime.CompilerServices;
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

    public async Task<ChatResponse> CompleteAsync(IEnumerable<ChatRequestMessage> messages, CancellationToken cancellationToken = default)
    {
        var request = new ChatRequest
        {
            Messages = messages,
            Model = "deepseek-reasoner",
            Stream = false,
        };

        using var response = await _httpClient.PostAsJsonAsync("v1/chat/completions", request, ChatApiSerializationContext.Default.ChatRequest, cancellationToken).ConfigureAwait(false);
        var result = await response.EnsureSuccessStatusCode().Content
            .ReadFromJsonAsync(ChatApiSerializationContext.Default.ChatResponse, cancellationToken)
            .ConfigureAwait(false);
        return result ?? throw new Exception("Top level null");
    }

    public async IAsyncEnumerable<ChatResponseChunk> ChunkedCompleteAsync(IEnumerable<ChatRequestMessage> messages, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var request = new ChatRequest
        {
            Messages = messages,
            Model = "deepseek-reasoner",
            Stream = true,
        };

        using var requestMessage = new HttpRequestMessage(HttpMethod.Post, "v1/chat/completions")
        {
            Content = JsonContent.Create(request, ChatApiSerializationContext.Default.ChatRequest)
        };

        using var response = await _httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
        var responseStream = await response.EnsureSuccessStatusCode().Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);

        var reader = SseParser.Create(responseStream, (name, value) =>
        {
            if (value.SequenceEqual("[DONE]"u8))
                return null;

            return JsonSerializer.Deserialize(value, ChatApiSerializationContext.Default.ChatResponseChunk);
        });

        await foreach (var item in reader.EnumerateAsync(cancellationToken).ConfigureAwait(false))
            if (item.Data != null)
                yield return item.Data;
    }
}
