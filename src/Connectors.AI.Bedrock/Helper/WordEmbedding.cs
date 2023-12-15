using Microsoft.ML.Transforms.Text;
using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectors.AI.Bedrock.Helper
{
    public class WordEmbedding
    {
        MLContext mlContext;
        public WordEmbedding()
        {
            // Create a new ML context, for ML.NET operations. It can be used for
            // exception tracking and logging, as well as the source of randomness.
            mlContext = new MLContext();
        }
        public float[] GetEmbedding(string sentence)
        {
            try
            {
                // Create an empty list as the dataset. The 'ApplyWordEmbedding' does
                // not require training data as the estimator ('WordEmbeddingEstimator')
                // created by 'ApplyWordEmbedding' API is not a trainable estimator.
                // The empty list is only needed to pass input schema to the pipeline.
                var emptySamples = new List<TextData>();

                // Convert sample list to an empty IDataView.
                var emptyDataView = mlContext.Data.LoadFromEnumerable(emptySamples);

                // A pipeline for converting text into a 150-dimension embedding vector
                // using pretrained 'SentimentSpecificWordEmbedding' model. The
                // 'ApplyWordEmbedding' computes the minimum, average and maximum values
                // for each token's embedding vector. Tokens in 
                // 'SentimentSpecificWordEmbedding' model are represented as
                // 50 -dimension vector. Therefore, the output is of 150-dimension [min,
                // avg, max].
                //
                // The 'ApplyWordEmbedding' API requires vector of text as input.
                // The pipeline first normalizes and tokenizes text then applies word
                // embedding transformation.
                var textPipeline = mlContext.Transforms.Text.NormalizeText("Text")
                    .Append(mlContext.Transforms.Text.TokenizeIntoWords("Tokens",
                        "Text"))
                    .Append(mlContext.Transforms.Text.ApplyWordEmbedding("Features",
                        "Tokens", WordEmbeddingEstimator.PretrainedModelKind
                        .SentimentSpecificWordEmbedding));

                // Fit to data.
                var textTransformer = textPipeline.Fit(emptyDataView);

                // Create the prediction engine to get the embedding vector from the
                // input text/string.
                var predictionEngine = mlContext.Model.CreatePredictionEngine<TextData,
                    TransformedTextData>(textTransformer);

                // Call the prediction API to convert the text into embedding vector.
                var data = new TextData()
                {
                    Text = sentence
                };
                var prediction = predictionEngine.Predict(data);

                // Print the length of the embedding vector.
                //Console.WriteLine($"Number of Features: {prediction.Features.Length}");

                // Print the embedding vector.
                //Console.Write("Features: ");
                //foreach (var f in prediction.Features)
                //    Console.Write($"{f:F4} ");

                //  Expected output:
                //   Number of Features: 150
                //   Features: -1.2489 0.2384 -1.3034 -0.9135 -3.4978 -0.1784 -1.3823 -0.3863 -2.5262 -0.8950 ...
                return prediction.Features;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return default;
        }

        private class TextData
        {
            public string Text { get; set; }
        }

        private class TransformedTextData : TextData
        {
            public float[] Features { get; set; }
        }
    }
}
