using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Windows.Forms;
using Karpach.Remote.Commands.Interfaces;

namespace Karpach.Remote.Commands
{    
    public class WakeOnLanCommand: IRemoteCommand
    {
        private readonly LibSettings _libSettings = new LibSettings();
        private readonly WakeOnLanCommandSettings _settings;
        private bool _configured;

        public Guid Id => _settings.Id;

        public int Index { get; set; }

        public string CommandTitle => $"Wake-On-Lan - {_settings.PcName}";

        public bool Configured => _configured;

        public Image TrayIcon => Resources.Lan.ToBitmap();

        public string AssemblyName => Assembly.GetExecutingAssembly().GetName().Name;

        public WakeOnLanCommand()
        {
            _settings = _libSettings.WakeOnLanCommandSettings.Count > Index
                ? _libSettings.WakeOnLanCommandSettings[Index]
                : new WakeOnLanCommandSettings { Id = Guid.NewGuid() };
        }

        public void RunCommand(object sender, EventArgs e)
        {            
            string macAddress = _settings?.MacAddress;                        

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

        public void ShowSettings()
        {            
            var dlg = new WakeOnLanSettingsForm(_settings);
            DialogResult result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (_libSettings.WakeOnLanCommandSettings.All(s => s.Id != Id))
                {
                    _libSettings.WakeOnLanCommandSettings.Add(dlg.Settings);
                }
                _configured = true;
                _libSettings.Save();
            }
        }

        public IRemoteCommand Create(int index)
        {
            return new WakeOnLanCommand { Index = index};
        }

        public bool CanCreate()
        {
            return true;
        }

        public string Version
        {
            get
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
                if (attributes.Length > 0 && attributes[0] is AssemblyFileVersionAttribute)
                {
                    return ((AssemblyFileVersionAttribute)attributes[0]).Version;
                }
                return "N/A";
            }
        }        
    }
}
