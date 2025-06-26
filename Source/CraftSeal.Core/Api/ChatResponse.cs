using System.Text.Json.Serialization;

namespace CraftSeal.Api;

public abstract class ChatResponseBase
{
    public required Guid Id { get; init; }

    [JsonConverter(typeof(UnixTimestampSecondConverter))]
    public required DateTimeOffset Created { get; init; }

    public required string Model { get; init; }

    public string? SystemFingerprint { get; init; }

    public required string Object { get; init; }
}

public class ChatResponse : ChatResponseBase
{
    public required CompletionChoice[] Choices { get; init; }
}

public class ChatResponseChunk : ChatResponseBase
{
    public required CompletionDeltaChoice[] Choices { get; init; }
}

public class CompletionChoice
{
    public required CompletionFinishingReason FinishReason { get; init; }

    public required int Index { get; init; }

    public required CompletionMessage Message { get; init; }
}

public class CompletionDeltaChoice
{
    public required CompletionFinishingReason FinishReason { get; init; }

    public required int Index { get; init; }

    public required CompletionMessage Delta { get; init; }
}

[JsonConverter(typeof(JsonStringEnumConverter<CompletionFinishingReason>))]
public enum CompletionFinishingReason
{
    stop,
    length,
    content_filter,
    insufficient_system_resource,
}

public class CompletionMessage
{
    public required string? Content { get; init; }

    public string? ReasoningContent { get; init; }

    public ToolCallMessage[]? ToolCalls { get; init; }

    public required string Role { get; init; }
}

public class ToolCallMessage
{
    public required string Id { get; init; }

    public required ToolType Type { get; init; }

    public FunctionCall? Function { get; init; }
}

public class FunctionCall
{
    public required string Name { get; init; }

    public required string Arguments { get; init; } // JSON，需验证
}
