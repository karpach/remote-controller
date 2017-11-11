using Karpach.Remote.Commands.Base;

namespace Karpach.Remote.Commands.Shutdown
{
    public class ShutdownSettings: CommandSettingsBase
    {
        public ShutdownCommandType CommandType { get; set; }
    }
}