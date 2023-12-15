using System;
namespace Connectors.AI.Bedrock
{
	public class VerifyHelper
	{
		public VerifyHelper()
		{
		}
        public static void NotNull(object item)
        {
            if (item is null)
            {
                throw new Exception($"{nameof(item)} cannot be null");
            }
        }
        public static void NotNull(Uri url)
        {
            if (url is null)
            {
                throw new Exception("Url cannot be null");
            }
        }
        public static void NotNull(string url)
		{
			if (url is null)
			{
				throw new Exception("Url cannot be null");
			}
		}
        public static void NotNullOrWhiteSpace(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new Exception("Url cannot be null or whitespace");
            }
        }
        
    }
}

