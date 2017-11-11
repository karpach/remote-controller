using Karpach.Remote.Commands.Base;

namespace Karpach.Remote.Commands.HttpRequest
{
    public class HttpRequestSettings: CommandSettingsBase
    {        
        public string CommandName { get; set; }
        public string Url { get; set; }
    }
}