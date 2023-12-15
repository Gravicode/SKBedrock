// Copyright (c) Microsoft. All rights reserved.

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Connectors.AI.Bedrock.Skills;

/// <summary>
/// class for request token count
/// </summary>
public class TokenRequest
{
    [JsonPropertyName("contents")]
    public Content[] Contents { get; set; } = new Content[] { new() };
}
/// <summary>
/// contain collection of text for token counting
/// </summary>
public class Content
{
    [JsonPropertyName("parts")]
    public Part[] Parts { get; set; } = new Part[] { new() };
}
/// <summary>
/// text 
/// </summary>
public class Part
{
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;
}
