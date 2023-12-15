// Copyright (c) Microsoft. All rights reserved.

using Microsoft.SemanticKernel.AI.ChatCompletion;
using Microsoft.SemanticKernel.Text;

namespace Microsoft.SemanticKernel.Connectors.AI.Bedrock.ChatCompletion;

/// <summary>
/// OpenAI Chat content
/// See https://platform.openai.com/docs/guides/chat for details
/// </summary>
public class BedrockChatHistory : ChatHistory
{
    /// <summary>
    /// Create a new and empty chat history
    /// </summary>
    /// <param name="assistantInstructions">Optional instructions for the assistant</param>
    public BedrockChatHistory(string? assistantInstructions = null)
    {
        if (!string.IsNullOrWhiteSpace( assistantInstructions))
        {
            this.AddSystemMessage(assistantInstructions);
        }
    }
}
