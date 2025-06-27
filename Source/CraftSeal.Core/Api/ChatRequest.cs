using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace CraftSeal.Api;

public class ChatRequest
{
    public required IEnumerable<ChatRequestMessage> Messages { get; init; }

    public required string Model { get; init; }

    public double? FrequencyPenalty { get; init; }

    public int? MaxTokens { get; init; }

    public double? PresencePenalty { get; init; }

    public ResponseFormat? ResponseFormat { get; init; }

    public IEnumerable<string>? Stop { get; init; }

    public bool Stream { get; init; }

    public StreamOptions? StreamOptions { get; init; }

    public double? Temperature { get; init; }

    public double? TopP { get; init; }

    public IEnumerable<ToolInfo>? Tools { get; init; }

    public ToolChoice? ToolChoice { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool Logprobs { get; init; }

    public int? TopLogprobs { get; init; }
}

public record class StreamOptions(bool IncludeUsage);

public record class ResponseFormat(ResponseFormatType Type);

public enum ResponseFormatType { text, json_object }

public class ToolInfo
{
    public required ToolType Type { get; init; }
}

public class FunctionTool
{
    public required string Description { get; init; }

    public required string Name { get; init; }

    public required JsonNode Parameters { get; init; }
}

public enum ToolType { function }

public enum ToolChoice { auto, none, required }

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

    public string? ReasoningContent { get; init; }
}

public class ToolMessage : ChatRequestMessage
{
    public required string ToolCallId { get; init; }
}
