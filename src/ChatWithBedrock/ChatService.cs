using Amazon.Bedrock.Model;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.AI.Bedrock.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI;

namespace ChatWithBedrock
{
    public class ChatService
    {
        public bool IsProcessing { get; set; } = false;
        string systemMessage;
        Dictionary<string, ISKFunction> ListFunctions = new Dictionary<string, ISKFunction>();

        IKernel kernel { set; get; }
        BedrockChatHistory chat;
        private BedrockChatCompletion chatBedrock;
        public bool IsConfigured { get; set; } = false;
        public ChatService()
        {

            // Configure AI backend used by the kernel          
            kernel = new KernelBuilder()
               .WithAWSBedrockLibrary(AppConstants.AccessKeyId,AppConstants.SecretAccessKey,AppConstants.Region)
               .Build();
        }

        public void SelectModel(FoundationModelSummary model)
        {
            kernel = new KernelBuilder()
           .WithAWSBedrockLibrary(AppConstants.AccessKeyId, AppConstants.SecretAccessKey, AppConstants.Region)
           .WithBedrockChatCompletionService(model, apiKey: "", serviceId: "chat-Bedrock")
           .Build();

        }

        public void SetupSkill(string Context = "")
        {
            // Get AI service instance used to manage the user chat
            chatBedrock = kernel.GetService<BedrockChatCompletion>();
            systemMessage = string.IsNullOrEmpty(Context) ? "You're chatting with a user. You are an expert of everything. You can answer politely like a professional." : Context;
            chat = (BedrockChatHistory)chatBedrock.CreateNewChat(systemMessage);
            IsConfigured = true;
        }

        public void Reset()
        {
            chat = (BedrockChatHistory)chatBedrock.CreateNewChat(systemMessage);
        }

        public async Task<string> Chat(string userMessage)
        {
            if (!IsConfigured) SetupSkill();

            string Result = string.Empty;
            if (IsProcessing) return Result;

            try
            {
                IsProcessing = true;
                //1.Ask the user for a message. The user enters a message.Add the user message into the Chat History object.
                //Console.WriteLine($"User: {userMessage}");
                chat.AddUserMessage(userMessage);

                // 2. Send the chat object to AI asking to generate a response. Add the bot message into the Chat History object.
                string assistantReply = await chatBedrock.GenerateMessageAsync(chat, new OpenAIRequestSettings()
                {
                    MaxTokens = 2000,
                    Temperature = 0.7,
                    TopP = 0.5
                });
                chat.AddAssistantMessage(assistantReply);
                //Console.WriteLine(assistantReply);
                Result = assistantReply;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error With Bedrock: {ex}");
            }
            finally
            {
                IsProcessing = false;
            }
            return Result;
        }

    }
}
