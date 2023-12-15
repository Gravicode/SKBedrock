using Amazon.Bedrock;
using Amazon.Bedrock.Model;
using DotnetFMPlayground.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectors.AI.Bedrock.Helper
{
    public class BedrockModelInfo
    {
        public string ModelName { get; set; }
        public string Provider { get; set; }
        public string ModelId { get; set; }
        public static async Task<List<FoundationModelSummary>> GetFoundationalModels()
        {
            IEnumerable<FoundationModelSummary> foundationModels;
            try
            {
                var BedrockClient = MinimalContainer.Get<AmazonBedrockClient>();
                foundationModels = (await BedrockClient.ListFoundationModelsAsync(new ListFoundationModelsRequest())).ModelSummaries.Where(x => x.OutputModalities.Contains("TEXT") && ModelIds.IsSupported(x.ModelId));
                return foundationModels.ToList();
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.ToString());
                throw new Exception("Please initialize the library first, with method: WithAWSBedrockLibrary");
            }
            return default;
        }
    }
}
