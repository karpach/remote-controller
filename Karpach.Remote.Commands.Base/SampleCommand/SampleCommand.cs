using System;
using System.Drawing;
using Karpach.Remote.Commands.Base;
using Karpach.Remote.Commands.Interfaces;
using System.Windows.Forms;

namespace SampleCommand
{
    public class SampleCommand : CommandBase
    {
        public SampleCommand():base(null)
        {
        }

        public SampleCommand(Guid? id) : base(id)
        {
        }

        public override string CommandTitle => ConfiguredValue ? ((SampleCommandSettings)Settings).CommandName : $"Sample Command - {NotConfigured}";
        protected override Type SettingsType => typeof(SampleCommandSettings);
        public override Image TrayIcon { get; }
        public override void RunCommand(params object[] parameters)
        {
            MessageBox.Show("Sample Command", "Running ...", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public override void ShowSettings()
        {
            var dlg = new SampleCommandSettingsForm((SampleCommandSettings)Settings);
            DialogResult result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                LibSettings[Id] = dlg.Settings;
                ConfiguredValue = true;
            }
        }

        public override IRemoteCommand Create(Guid id)
        {
            return new SampleCommand(id);
        }
    }
}
