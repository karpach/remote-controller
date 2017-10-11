using System;

namespace Karpach.Remote.Commands.WakeOnLan
{
    public class WakeOnLanCommandSettings
    {
        public Guid Id { get; set; }
        public string PcName { get; set; }
        public string MacAddress { get; set; }
    }
}