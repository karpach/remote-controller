using Karpach.Remote.Commands.Base;

namespace Karpach.Remote.Commands.WakeOnLan
{
    public class WakeOnLanCommandSettings : CommandSettingsBase
    {
        public string PcName { get; set; }
        public string MacAddress { get; set; }
    }
}