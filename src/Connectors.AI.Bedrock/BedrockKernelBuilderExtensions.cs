// Copyright (c) Microsoft. All rights reserved.

using System.Net.Http;
using Amazon;
using Amazon.Bedrock.Model;
using Connectors.AI.Bedrock.Helper;
using Connectors.AI.Bedrock.TextEmbedding;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.AI.Embeddings;
using Microsoft.SemanticKernel.AI.TextCompletion;
using Microsoft.SemanticKernel.Connectors.AI.Bedrock.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AI.Bedrock.TextCompletion;

#pragma warning disable IDE0130
// ReSharper disable once CheckNamespace - Using NS of KernelConfig
namespace Microsoft.SemanticKernel;
#pragma warning restore IDE0130

/// <summary>
/// Provides extension methods for the <see cref="KernelBuilder"/> class to configure Bedrock connectors.
/// </summary>
public static class BedrockKernelBuilderExtensions
{
    /// <summary>
    /// Registers an Bedrock text completion service with the specified configuration.
    /// </summary>
    /// <param name="builder">The <see cref="KernelBuilder"/> instance.</param>
    /// <param name="model">The name of the Bedrock model.</param>
    /// <param name="apiKey">The API key required for accessing the Bedrock service.</param>
    /// <param name="endpoint">The endpoint URL for the text completion service.</param>
    /// <param name="serviceId">A local identifier for the given AI service.</param>
    /// <param name="httpClient">The optional <see cref="HttpClient"/> to be used for making HTTP requests.
    /// If not provided, a default <see cref="HttpClient"/> instance will be used.</param>
    /// <returns>The modified <see cref="KernelBuilder"/> instance.</returns>
    public static KernelBuilder WithBedrockTextCompletionService(this KernelBuilder builder,
        FoundationModelSummary model,
        string? apiKey = null,
        string? endpoint = null,
        string? serviceId = null,
        HttpClient? httpClient = null)
    {
        builder.WithAIService<ITextCompletion>(serviceId, (parameters) =>
            new BedrockTextCompletion(
                model.ModelId
               ));
        
        return builder;
    }


    public static KernelBuilder WithBedrockChatCompletionService(this KernelBuilder builder,
        FoundationModelSummary model,
        string apiKey = null, string? serviceId=null)
    {
        builder.WithAIService<BedrockChatCompletion>(serviceId, (parameters) =>
            new BedrockChatCompletion(
                model.ModelId));

        return builder;
    }
   

    /// <summary>
    /// Registers an Bedrock text embedding generation service with the specified configuration.
    /// </summary>
    /// <param name="builder">The <see cref="KernelBuilder"/> instance.</param>
    /// <param name="model">The name of the Bedrock model.</param>
    /// <param name="apiKey">API Key for Bedrock.</param>
    /// <param name="serviceId">A local identifier for the given AI service.</param>
    /// <returns>The <see cref="KernelBuilder"/> instance.</returns>
    public static KernelBuilder WithBedrockTextEmbeddingGenerationService(this KernelBuilder builder,
        string model,
        string apiKey,
        string? serviceId = null)
    {
        builder.WithAIService<ITextEmbeddingGeneration>(serviceId, (parameters) =>
            new BedrockTextEmbeddingGeneration());

        return builder;
    }

    /// <summary>
    /// Registers an Bedrock text embedding generation service with the specified configuration.
    /// </summary>
    /// <param name="builder">The <see cref="KernelBuilder"/> instance.</param>
    /// <param name="model">The name of the Bedrock model.</param>
    /// <param name="httpClient">The optional <see cref="HttpClient"/> instance used for making HTTP requests.</param>
    /// <param name="endpoint">The endpoint for the text embedding generation service.</param>
    /// <param name="serviceId">A local identifier for the given AI serviceю</param>
    /// <returns>The <see cref="KernelBuilder"/> instance.</returns>
    public static KernelBuilder WithBedrockTextEmbeddingGenerationService(this KernelBuilder builder,
        string model,
        HttpClient? httpClient = null,
        string? endpoint = null,
        string? serviceId = null)
    {
        builder.WithAIService<ITextEmbeddingGeneration>(serviceId, (parameters) =>
            new BedrockTextEmbeddingGeneration());

        return builder;
    }
    
    /// <summary>
    /// Initialize AWS Bedrock Library
    /// </summary>
    /// <param name="builder">kernel</param>
    /// <param name="AccessKeyId">AWS Access Key</param>
    /// <param name="SecretAccessKey">AWS Secret Access Key</param>
    /// <param name="region">AWS Region</param>
    /// <returns></returns>
    public static KernelBuilder WithAWSBedrockLibrary(this KernelBuilder builder,
        string AccessKeyId, string SecretAccessKey, RegionEndpoint region)
    {
        AppConstants.SetupAWSLibrary(AccessKeyId, SecretAccessKey, region);
        return builder;
    }
}
