// Copyright (c) Microsoft. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Connectors.AI.Bedrock.Helper;
using Microsoft.SemanticKernel.AI;
using Microsoft.SemanticKernel.AI.Embeddings;
using Microsoft.SemanticKernel.Diagnostics;
using Microsoft.SemanticKernel.Services;
using SharpToken;

namespace Connectors.AI.Bedrock.TextEmbedding;

/// <summary>
/// Bedrock embedding generation service.
/// </summary>
public sealed class BedrockTextEmbeddingGeneration : ITextEmbeddingGeneration, IDisposable
{
    //private const string HttpUserAgent = "Microsoft-Semantic-Kernel";

    //private readonly string _model = "embedding-001";//"embedding-gecko-001";
    //private readonly string? _endpoint = "https://generativelanguage.googleapis.com/v1beta/models";//"https://generativelanguage.googleapis.com/v1beta2/models";
    //private readonly HttpClient _httpClient;
    //private readonly string? _apiKey;
    private readonly Dictionary<string, string> _attributes = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="BedrockTextEmbeddingGeneration"/> class.
    /// Using default <see cref="HttpClientHandler"/> implementation.
    /// </summary>
    
    public BedrockTextEmbeddingGeneration()//(Uri endpoint, string model, string apiKey, HttpClient? httpClient = null)
    {
        //VerifyHelper.NotNull(endpoint);
        //VerifyHelper.NotNullOrWhiteSpace(model);
        //VerifyHelper.NotNullOrWhiteSpace(apiKey);

        //this._endpoint = endpoint.AbsoluteUri;
        //this._model = model;
        //this._apiKey = apiKey;
        //this._httpClient = httpClient ?? new HttpClient();
        //this._attributes.Add(IAIServiceExtensions.ModelIdKey, this._model);
        //this._attributes.Add(IAIServiceExtensions.EndpointKey, this._endpoint);
    }

    public IReadOnlyDictionary<string, string> Attributes => this._attributes;
    /// <inheritdoc/>
    public async Task<IList<ReadOnlyMemory<float>>> GenerateEmbeddingsAsync(IList<string> data, CancellationToken cancellationToken = default)
    {
        return await this.ExecuteEmbeddingRequestAsync(data, cancellationToken).ConfigureAwait(false);
    }

    #region private ================================================================================

    /// <summary>
    /// Performs HTTP request to given endpoint for embedding generation.
    /// </summary>
    /// <param name="data">Data to embed.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests. The default is <see cref="CancellationToken.None"/>.</param>
    /// <returns>List of generated embeddings.</returns>
    /// <exception cref="AIException">Exception when backend didn't respond with generated embeddings.</exception>
    private async Task<IList<ReadOnlyMemory<float>>> ExecuteEmbeddingRequestAsync(IList<string> data, CancellationToken cancellationToken)
    {
        try
        {
            // Get encoding by encoding name
            var wordembed = new WordEmbedding();
            var embededList = new List<float>();
            foreach(var text in data)
            {
                var embedding = wordembed.GetEmbedding(text);
                embededList.AddRange(embedding);
            }
            return new List<ReadOnlyMemory<float>>() { new ReadOnlyMemory<float>(embededList.ToArray())};
        }
        catch (Exception e) 
        {
            throw new SKException(
                $"Something went wrong: {e.Message}", e);
        }
    }

    public void Dispose()
    {
       
    }

    #endregion
}
