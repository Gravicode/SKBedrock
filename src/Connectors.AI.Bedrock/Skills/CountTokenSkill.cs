//using Microsoft.SemanticKernel.SkillDefinition;
using System.ComponentModel;
using Microsoft.SemanticKernel;
using SharpToken;

namespace Connectors.AI.Bedrock.Skills;

public sealed class TokenSkill:IDisposable
{
   
    /// <summary>
    /// Initializes a new instance of the Token Skill
    /// </summary>
   
    public TokenSkill()//(string model, string apiKey, HttpClient? httpClient = null, string? endpoint = null)
    {
       
    }   
    /// <summary>
    /// count tokens from text.
    /// </summary>
    /// <example>
    /// SKContext["input"] = "hello world"
    /// {{token.countToken $input}} => 2
    /// </example>
    /// <param name="input"> The string to count. </param>
    /// <param name="cancellationToken"> cancellation token. </param>
    /// <returns> The token count. </returns>
    [SKFunction, Description("count token from text.")]
    public async Task<int> CountTokens(string input, CancellationToken cancellationToken = default)
    {
        try
        {
            // Get encoding by encoding name
            var encoding = GptEncoding.GetEncoding("cl100k_base");
            var encoded = encoding.Encode(input);
            return encoded.Count;
        }
        catch (Exception e) 
        {
            throw new Exception(
                
                $"Something went wrong: {e.Message}", e);
        }
    }

    public void Dispose()
    {
       
    }

}
