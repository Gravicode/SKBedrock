// Copyright (c) Microsoft. All rights reserved.

using System.Text.Json.Serialization;

namespace Connectors.AI.Bedrock.TextCompletion;
/// <summary>
/// Collection of filter that triggers error
/// </summary>
public class TextCompletionError
{
    [JsonPropertyName("filters")]
    public Filter[]? Filters { get; set; }
}

/// <summary>
/// Reason for error (not returning response)
/// </summary>
public class Filter
{
    [JsonPropertyName("reason")]
    public string? Reason { get; set; }
}
