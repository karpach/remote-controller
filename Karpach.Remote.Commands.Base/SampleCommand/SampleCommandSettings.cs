using Karpach.Remote.Commands.Base;

namespace SampleCommand
{
    public class SampleCommandSettings : CommandSettingsBase
    {
        public string CommandName { get; set; }
        public int? ExecutionDelay { get; set; }
    }
}   