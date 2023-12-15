using System;
using System.Diagnostics.CodeAnalysis;

namespace Connectors.AI.Bedrock.Helper
{
    
    public static class StringExtensions
    {
        public static string NormalizeLineEndings(this string src)
        {
#if NET6_0_OR_GREATER
        return src.ReplaceLineEndings("\n");
#else
            return src.Replace("\r\n", "\n");
#endif
        }

        public static bool IsNullOrEmpty(this string? data)
        {
            return string.IsNullOrEmpty(data);
        }

        public static bool IsNullOrWhitespace(this string? data)
        {
            return string.IsNullOrWhiteSpace(data);
        }
    }
}

