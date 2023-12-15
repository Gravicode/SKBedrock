// Copyright (c) Microsoft. All rights reserved.

using System.Text.Json.Serialization;

namespace Connectors.AI.Bedrock.TextCompletion;
public class TextCompletionResponse
{
    [JsonPropertyName("text")]
    public string Text { get; set; }
}
