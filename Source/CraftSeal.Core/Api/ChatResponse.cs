using System.Diagnostics.CodeAnalysis;

namespace CraftSeal.Api;

public abstract class ChatResponseBase
{
    public required Guid Id { get; init; }

    public required DateTimeOffset Created { get; init; }

    public required string Model { get; init; }

    public string? SystemFingerprint { get; init; }

    public string? Object { get; init; }
}

public class ChatResponse : ChatResponseBase
{
    public required IReadOnlyList<CompletionChoice> Choices { get; init; }
}

public class ChatResponseChunk : ChatResponseBase
{
    public required IReadOnlyList<CompletionDeltaChoice> Choices { get; init; }
}

public class CompletionChoice
{
    public CompletionFinishingReason? FinishReason { get; init; }

    public required int Index { get; init; }

    public required CompletionMessage Message { get; init; }
}

public class CompletionDeltaChoice
{
    public CompletionFinishingReason? FinishReason { get; init; }

    public required int Index { get; init; }

    public required CompletionMessage Delta { get; init; }
}

public enum CompletionFinishingReason
{
    stop,
    length,
    content_filter,
    insufficient_system_resource,
}

public class CompletionMessage
{
    public string? Content { get; init; }

    public string? ReasoningContent { get; init; }

    public IReadOnlyList<ToolCallMessage>? ToolCalls { get; init; }

    public string? Role { get; init; }
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

    [StringSyntax(StringSyntaxAttribute.Json)]
    public required string Arguments { get; init; } // JSON，需验证
}
