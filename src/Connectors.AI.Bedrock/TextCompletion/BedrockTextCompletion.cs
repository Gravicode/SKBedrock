// Copyright (c) Microsoft. All rights reserved.

using System.Runtime.CompilerServices;
using System.Text.Json;
using Amazon.Bedrock;
using Amazon.BedrockRuntime;
using DotnetFMPlayground.Core;
using Connectors.AI.Bedrock.Helper;
using Connectors.AI.Bedrock.TextCompletion;
using DotnetFMPlayground.Core.Models;

using Microsoft.SemanticKernel.AI;
using Microsoft.SemanticKernel.AI.TextCompletion;
using Microsoft.SemanticKernel.Diagnostics;
using SharpToken;

namespace Microsoft.SemanticKernel.Connectors.AI.Bedrock.TextCompletion;

/// <summary>
/// Bedrock text completion service.
/// </summary>
public sealed class BedrockTextCompletion : ITextCompletion,IDisposable
{
   
    private readonly string _model;
   
    private readonly Dictionary<string, string> _attributes = new();
    AmazonBedrockRuntimeClient BedrockRuntimeClient;
    AmazonBedrockClient BedrockClient1;
    public IReadOnlyDictionary<string, string> Attributes => _attributes;

    public void Dispose()
    {
    
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="BedrockTextCompletion"/> class.
    /// Using default <see cref="HttpClientHandler"/> implementation.
    /// </summary>
    /// <param name="endpoint">Endpoint for service API call.</param>
    /// <param name="model">Model to use for service API call.</param>
    public BedrockTextCompletion(string model)
    {
      
        this._model = model;
        this.BedrockRuntimeClient = MinimalContainer.Get<AmazonBedrockRuntimeClient>();
        this.BedrockClient1 = MinimalContainer.Get<AmazonBedrockClient>();
        
    }

    public async IAsyncEnumerable<ITextStreamingResult> GetStreamingCompletionsAsync(
        string text,
        AIRequestSettings requestSettings,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        foreach (var completion in await this.ExecuteGetCompletionsAsync(text, cancellationToken).ConfigureAwait(false))
        {
            yield return completion;
        }
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<ITextResult>> GetCompletionsAsync(
        string text,
        AIRequestSettings requestSettings,
        CancellationToken cancellationToken = default)
    {
       return await this.ExecuteGetCompletionsAsync2(text, cancellationToken).ConfigureAwait(false);
       
    }

    #region private ================================================================================
    private async Task<IReadOnlyList<ITextResult>> ExecuteGetCompletionsAsync2(string text, CancellationToken cancellationToken = default)
    {
        try
        {
            DotnetFMPlayground.Core.Models.Prompt prompt = new();
            prompt.Add(new PromptItem(PromptItemType.User, text));

          
                var outputText = "Thinking...";
                var response = await BedrockRuntimeClient.InvokeModelAsync(
                    this._model,
                    prompt,
                    new()
                    {
                    { "max_tokens_to_sample", 2048 }
                    }
                );
                outputText = response.GetResponse();

            TextCompletionResponse completionResponse = new TextCompletionResponse();
            completionResponse.Text = outputText;

            if (completionResponse is null)
            {
                throw new SKException("Unexpected response from model")
                {
                    Data = { { "ResponseData", "No response" } },
                };
            }

            return new List<TextCompletionResult>() { new TextCompletionResult(completionResponse) };

        }
        catch (Exception e)
        {
            throw new SKException(
                $"Something went wrong: {e.Message}", e);
        }
    }
    private async Task<IReadOnlyList<ITextStreamingResult>> ExecuteGetCompletionsAsync(string text, CancellationToken cancellationToken = default)
    {
        try
        {
            DotnetFMPlayground.Core.Models.Prompt prompt = new();
            prompt.Add(new PromptItem(PromptItemType.User, text));


            var outputText = "Thinking...";
            var response = await BedrockRuntimeClient.InvokeModelAsync(
                this._model,
                prompt,
                new()
                {
                    { "max_tokens_to_sample", 2048 }
                }
            );
            outputText = response.GetResponse();

            TextCompletionResponse completionResponse = new TextCompletionResponse();
            completionResponse.Text = outputText;

            if (completionResponse is null)
            {
                throw new SKException("Unexpected response from model")
                {
                    Data = { { "ResponseData", "No response" } },
                };
            }

            if (completionResponse is null)
            {
                throw new SKException( "Unexpected response from model")
                {
                    Data = { { "ResponseData", "No Response" } },
                };
            }

             return new List<ITextStreamingResult>() { new TextCompletionStreamingResult(completionResponse) };
        }
        catch (Exception e) 
        {
            throw new SKException(
                $"Something went wrong: {e.Message}", e);
        }
    }
    
    
    #endregion
}
