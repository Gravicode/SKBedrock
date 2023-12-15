using Amazon;
using Amazon.Bedrock;
using Amazon.BedrockAgent;
using Amazon.BedrockAgentRuntime;
using Amazon.BedrockRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectors.AI.Bedrock.Helper
{
    public class AppConstants
    {

        public static void SetupAWSLibrary(string AccessKeyId, string SecretAccessKey, RegionEndpoint region)
        {
            
            MinimalContainer.Register<AmazonBedrockRuntimeClient>(
                new AmazonBedrockRuntimeClient(AccessKeyId, SecretAccessKey, new AmazonBedrockRuntimeConfig()
                {
                    RegionEndpoint = region
                }));
            MinimalContainer.Register<AmazonBedrockClient>(
                new AmazonBedrockClient(AccessKeyId, SecretAccessKey, new AmazonBedrockConfig()
                {
                    RegionEndpoint = region
                }));

            MinimalContainer.Register<AmazonBedrockAgentRuntimeClient>(
                new AmazonBedrockAgentRuntimeClient(AccessKeyId, SecretAccessKey, region)
                );

            MinimalContainer.Register<AmazonBedrockAgentClient>(new(AccessKeyId, SecretAccessKey, region));
        }
    }
}
