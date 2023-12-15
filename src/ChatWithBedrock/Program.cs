using Amazon.Bedrock.Model;
using Connectors.AI.Bedrock.Helper;

namespace ChatWithBedrock
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var chat = new ChatService();
           
            var models = await BedrockModelInfo.GetFoundationalModels();
            Console.WriteLine("--------Select Bedrock Foundational Model---------");
            Console.WriteLine("--Make sure you have activated the model first!--");
            var no = 1;
            foreach(var item in models)
            {
                Console.WriteLine($"{no++}. {item.ModelName} by {item.ProviderName}");
            }
            var select = 0;
            do
            {
                Console.Write("select: ");
                var selstr = Console.ReadLine();
                int.TryParse(selstr, out select);
                select--;
            }
            while (select < 0 && select >= models.Count);
            //select model
            var selModel = models[select];
            chat.SelectModel(selModel);
            //start chat
            Console.WriteLine($"Selected Model >> {selModel.ModelName}");
            Console.WriteLine("--CHAT WITH Bedrock WITH Semantic Kernel--");
            var persona = "you are cute assistant with friendly attitude, you can answer anything with funny way.";
            Console.WriteLine($"ASSISTANT PERSONA: {persona}");
            Console.WriteLine($"----------------------------");
            Console.WriteLine($"-- type 'exit' to quit !! --");
            Console.WriteLine($"----------------------------");
            chat.SetupSkill(persona);
            while (true) {
                Console.Write("Q: ");
                var question = Console.ReadLine();
                if(question == "exit")
                {
                    Console.WriteLine("System: bye, thank you..");
                    break;
                }
                if (question != null)
                {
                    var answer = await chat.Chat(question);
                    Console.WriteLine($"A: {answer}");
                }
            }
        }
    }
}