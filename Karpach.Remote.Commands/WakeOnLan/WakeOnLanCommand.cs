using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Windows.Forms;
using Karpach.Remote.Commands.Interfaces;

namespace Karpach.Remote.Commands.WakeOnLan
{    
    public class WakeOnLanCommand: IRemoteCommand
    {
        private readonly LibSettings _libSettings = new LibSettings();
        private readonly WakeOnLanCommandSettings _settings;
        private bool _configured;
        private const string NotConfigured = "Not Configured";

        public Guid Id => _settings.Id;

        public string CommandTitle =>  $"Wake-On-Lan - { (_configured ? _settings.PcName : NotConfigured )}";

        public bool Configured => _configured;

        public Image TrayIcon => Resources.Lan.ToBitmap();

        public string AssemblyName => Assembly.GetExecutingAssembly().GetName().Name;

        public WakeOnLanCommand():this(null)
        {            
        }

        private WakeOnLanCommand(Guid? id)
        {
            if (id.HasValue)
            {                
                WakeOnLanCommandSettings settings = _libSettings[id.Value] as WakeOnLanCommandSettings;
                _configured = settings != null;
                _settings = settings ?? new WakeOnLanCommandSettings { Id = id.Value };                
            }
            else
            {                
                WakeOnLanCommandSettings[] allSettings = _libSettings.GetValues<WakeOnLanCommandSettings>();
                _configured = allSettings.Length > 0;
                _settings = _configured ? allSettings[0] : new WakeOnLanCommandSettings { Id = Guid.NewGuid() };
            }                        
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
                _libSettings[Id] = dlg.Settings;                
                _configured = true;                
            }
        }

        public IRemoteCommand Create(Guid id)
        {
            return new WakeOnLanCommand(id);
        }

        public bool Delete()
        {
            return _libSettings.Remove(Id);
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
