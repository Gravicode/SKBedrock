// Copyright (c) Microsoft. All rights reserved.

using Connectors.AI.Bedrock;
using Connectors.AI.Bedrock.Helper;
using Microsoft.SemanticKernel.AI;
using Microsoft.SemanticKernel.AI.ChatCompletion;
using Microsoft.SemanticKernel.Services;

namespace Microsoft.SemanticKernel.Connectors.AI.Bedrock.ChatCompletion;

/// <summary>
/// Bedrock chat completion client.
/// TODO: forward ETW logging to ILogger, see https://learn.microsoft.com/en-us/dotnet/azure/sdk/logging
/// </summary>
public sealed class BedrockChatCompletion :IAIService
{
    BedrockClient client { get; set; }

    public IReadOnlyDictionary<string, string> Attributes => _attributes;

    private readonly Dictionary<string, string> _attributes = new();
    /// <summary>
    /// Create an instance of the Bedrock chat completion connector
    /// </summary>
    /// <param name="modelId">Model name</param>
 

    public BedrockChatCompletion(
        string modelId
       )
    {
        VerifyHelper.NotNullOrWhiteSpace(modelId);
      
        this._attributes.Add(IAIServiceExtensions.ModelIdKey, modelId);
       
        this.client = new BedrockClient(modelId);
    }

    /// <inheritdoc/>
    public ChatHistory CreateNewChat(string? instructions = null)
    {
        return new BedrockChatHistory(instructions);
    }

    public async Task<string> GenerateMessageAsync(BedrockChatHistory chat, AIRequestSettings requestSettings = null, CancellationToken cancellationToken = default)
    {
        return await this.client.GetMessageAsync(chat, requestSettings, cancellationToken);
    }

}
