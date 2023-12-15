using Microsoft.SemanticKernel.AI.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AI.Bedrock.ChatCompletion;
using System.Text.Json;
using Microsoft.SemanticKernel.AI;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI;
using System.Text.Json.Serialization;
using Microsoft.SemanticKernel.Connectors.AI.Bedrock.TextCompletion;
using DotnetFMPlayground.Core.Models;
using Amazon.Bedrock;
using Amazon.BedrockRuntime;
using DotnetFMPlayground.Core;


namespace Connectors.AI.Bedrock.Helper
{
    public class BedrockClient
    {
        AmazonBedrockRuntimeClient BedrockRuntimeClient;
        AmazonBedrockClient BedrockClient1;
        string Model { set; get; } 
        public BedrockClient(string Model = "")
        {
            this.BedrockRuntimeClient = MinimalContainer.Get<AmazonBedrockRuntimeClient>();
            this.BedrockClient1 = MinimalContainer.Get<AmazonBedrockClient>();

            if (!string.IsNullOrEmpty(Model))
            {
                this.Model = Model;
            }
        }

        public virtual async Task<string> GetMessageAsync(BedrockChatHistory history, AIRequestSettings settings, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                OpenAIRequestSettings oaisetting = (OpenAIRequestSettings)settings;
                var context = history.Where(x => x.Role == AuthorRole.System).FirstOrDefault();

                var promptItems = new List<PromptItem>();
                history.ForEach(x =>
                {
                    if (x.Role != AuthorRole.System)
                    {
                        promptItems.Add(new PromptItem(x.Role == AuthorRole.Assistant ? PromptItemType.FMAnswer : PromptItemType.User, x.Content));
                    }
                });
                Prompt prompt = new();
                prompt.AddRange(promptItems);
               
                //json.GenerationConfig.Temperature = (float)oaisetting.Temperature;
                //json.GenerationConfig.StopSequences = oaisetting.StopSequences?.ToArray() ?? new string[0];
                //json.GenerationConfig.TopP = (float)oaisetting.TopP;
                //json.GenerationConfig.MaxOutputTokens = oaisetting.MaxTokens ?? 2048;
                var response = await BedrockRuntimeClient.InvokeModelAsync(
                                         Model,
                                         prompt,
                                         new()
                                         {
                                             { "max_tokens_to_sample", oaisetting.MaxTokens?? 2048 }
                                         }
                                     );
                
                if (response!=null)
                {
                    var resp = response.GetResponse();
                    promptItems.Add(new PromptItem(PromptItemType.FMAnswer, resp.Trim()));
                    return string.IsNullOrEmpty(resp) ? "Bedrock didn't answer" : resp.Trim();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
            return string.Empty;
        }


    }
   
}

