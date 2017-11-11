using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using Karpach.Remote.Commands.Base;
using Karpach.Remote.Commands.Interfaces;

namespace Karpach.Remote.Commands.WakeOnLan
{
    public class WakeOnLanCommand: CommandBase
    {                   
        public override string CommandTitle =>  $"Wake-On-Lan - { (ConfiguredValue ? ((WakeOnLanCommandSettings)Settings).PcName : NotConfigured )}";

        public override Image TrayIcon => Resources.Lan.ToBitmap();

        protected override Type SettingsType => typeof(WakeOnLanCommandSettings);

        public WakeOnLanCommand():base(null)
        {            
        }

        private WakeOnLanCommand(Guid id) : base(id)
        {            
        }

        public override void RunCommand(params object[] parameters)
        {            
            string macAddress = ((WakeOnLanCommandSettings)Settings)?.MacAddress;                        

            if (string.IsNullOrEmpty(macAddress))
            {
                return;                
            }            

            Byte[] datagram = new byte[102];

            for (int i = 0; i <= 5; i++)
            {
                datagram[i] = 0xff;
            }

            var macDigits = macAddress.Split(macAddress.Contains("-") ? '-' : ':');

            if (macDigits.Length != 6)
            {
                return;
            }

            int start = 6;
            for (int i = 0; i < 16; i++)
            {
                for (int x = 0; x < 6; x++)
                {
                    datagram[start + i * 6 + x] = (byte)Convert.ToInt32(macDigits[x], 16);
                }
            }

            UdpClient client = new UdpClient();
            client.Connect(IPAddress.Broadcast, 0);
            client.Send(datagram, datagram.Length);
        }        

        public override void ShowSettings()
        {            
            var dlg = new WakeOnLanSettingsForm((WakeOnLanCommandSettings)Settings);
            DialogResult result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                LibSettings[Id] = dlg.Settings;                
                ConfiguredValue = true;                
            }
        }

        public override IRemoteCommand Create(Guid id)
        {
            return new WakeOnLanCommand(id);
        }        
    }
}
