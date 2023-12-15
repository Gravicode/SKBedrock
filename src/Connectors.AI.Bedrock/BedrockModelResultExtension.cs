// Copyright (c) Microsoft. All rights reserved.

using Connectors.AI.Bedrock.TextCompletion;
using Microsoft.SemanticKernel.Orchestration;

#pragma warning disable IDE0130

namespace Microsoft.SemanticKernel;

public static class BedrockModelResultExtension
{
    /// <summary>
    /// Retrieves a typed <see cref="TextCompletionResponse"/> Bedrock result from PromptResult/>.
    /// </summary>
    /// <param name="resultBase">Current context</param>
    /// <returns>Bedrock result <see cref="TextCompletionResponse"/></returns>
    public static TextCompletionResponse GetBedrockResult(this ModelResult resultBase)
    {
        return resultBase.GetResult<TextCompletionResponse>();
    }
}
