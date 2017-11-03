using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Karpach.Remote.Commands.Interfaces;

namespace Karpach.Remote.Commands.Shutdown
{
    public class ShutdownCommand: CommandBase
    {
        public ShutdownCommand():base(null)
        {
        }

        private ShutdownCommand(Guid? id) : base(id)
        {
        }

        public override string CommandTitle => ConfiguredValue ? ((ShutdownSettings)Settings).CommandType.ToString() : $"Shutdown Command - {NotConfigured}";

        protected override Type SettingsType => typeof(ShutdownSettings);

        public override Image TrayIcon => Resources.Shutdown.ToBitmap();

        public override void RunCommand(params object[] parameters)
        {
            ShutdownCommandType commandType = ((ShutdownSettings)Settings).CommandType;            
            switch (commandType)
            {
                case ShutdownCommandType.Hibernate:
                    Application.SetSuspendState(PowerState.Hibernate, true, true);
                    break;
                case ShutdownCommandType.Shutdown:
                    Process.Start("shutdown", "/s /t 0");
                    break;
                case ShutdownCommandType.Suspend:
                    Application.SetSuspendState(PowerState.Suspend, true, true);
                    break;
                case ShutdownCommandType.Restart:
                    Process.Start("shutdown", "/r /t 0");
                    break;
            }
        }

        public override void ShowSettings()
        {
            var dlg = new ShutdownCommandSettingsForm((ShutdownSettings)Settings);
            DialogResult result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                LibSettings[Id] = dlg.Settings;
                ConfiguredValue = true;
            }
        }

        public override IRemoteCommand Create(Guid id)
        {
            return new ShutdownCommand(id);
        }
    }
}