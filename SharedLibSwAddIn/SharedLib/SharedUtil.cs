using System;

namespace Xarial.XCad.Examples
{
    public class SharedUtil
    {
        [Obsolete("Version 1.0.0.0")]
        public string GetMessage() => "Hello World!";

        [Obsolete("Version 1.1.0.0")]
        public string GetMessage(string name) => $"Hello {name}!";

        public string GetUserMessage(string name) => $"Hello {name}!";
    }
}
