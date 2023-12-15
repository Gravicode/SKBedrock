using System;
using System.Collections.Generic;
using System.Text;

namespace Connectors.AI.Bedrock.Helper
{
    public class ErrorBedrock
    {
        public ErrorInfo error { get; set; }
    }

    public class ErrorInfo
    {
        public int code { get; set; }
        public string message { get; set; }
        public string status { get; set; }
    }
}
