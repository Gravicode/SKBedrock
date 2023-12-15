// Copyright (c) Microsoft. All rights reserved.

using System.Text.Json.Serialization;

namespace Connectors.AI.Bedrock.Skills;


public class TokenResponse
{
    [JsonPropertyName("totalTokens")]
    public int TotalTokens { get; set; }
}
