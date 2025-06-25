using System.Text.Json.Serialization;

namespace CraftSeal.Api;

public class ChatRequest
{
    public required IEnumerable<ChatRequestMessage> Messages { get; init; }

    public required string Model { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public double FrequencyPenalty { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int MaxTokens { get; init; }

    public double PresencePenalty { get; init; }

    public bool Stream { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public StreamOptions? StreamOptions { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public double? Temperature { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public double? TopP { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public IEnumerable<ToolInfo>? Tools { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public ToolChoice? ToolChoice { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool Logprobs { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int? TopLogprobs { get; init; }
}

public record class StreamOptions(bool IncludeUsage);

public class ToolInfo
{
    public ToolType Type { get; init; }
}

[JsonConverter(typeof(JsonStringEnumConverter<ToolType>))]
public enum ToolType { function }

[JsonConverter(typeof(JsonStringEnumConverter<ToolChoice>))]
public enum ToolChoice { none, auto, required }

[JsonPolymorphic(TypeDiscriminatorPropertyName = "role")]
[JsonDerivedType(typeof(SystemMessage), "system")]
[JsonDerivedType(typeof(UserMessage), "user")]
[JsonDerivedType(typeof(AssistantMessage), "assistant")]
[JsonDerivedType(typeof(ToolMessage), "tool")]
public abstract class ChatRequestMessage
{
    public required string Content { get; init; }

    public string? Name { get; init; }
}

public class SystemMessage : ChatRequestMessage
{
}

public class UserMessage : ChatRequestMessage
{
}

public class AssistantMessage : ChatRequestMessage
{
    public bool Prefix { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ReasoningContent { get; init; }
}

public class ToolMessage : ChatRequestMessage
{
    public required string ToolCallId { get; init; }
}
