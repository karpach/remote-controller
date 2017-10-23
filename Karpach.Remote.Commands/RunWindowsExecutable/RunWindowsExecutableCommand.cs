using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Karpach.Remote.Commands.Interfaces;
using NLog;

namespace Karpach.Remote.Commands.RunWindowsExecutable
{
    public class RunWindowsExecutableCommand: CommandBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public override string CommandTitle => ConfiguredValue ? ((RunWindowsExecutableSettings)Settings).CommandName : $"Run Windows Executable Command - {NotConfigured}";

        public override Image TrayIcon => Resources.Exe.ToBitmap();

        protected override Type SettingsType => typeof(RunWindowsExecutableSettings);

        public RunWindowsExecutableCommand():base(null)
        {
        }

        private RunWindowsExecutableCommand(Guid? id) : base(id)
        {
        }        

        public override void RunCommand(params object[] parameters)
        {
            var settings = (RunWindowsExecutableSettings)Settings;            
            if (string.IsNullOrEmpty(settings.ExecutablePath))
            {
                Logger.Warn($"{settings.Id} Run Windows Executable Command no Excecutable Path supplied.");
                return;
            }
            Process p = new Process
            {
                StartInfo =
                {                    
                    FileName = settings.ExecutablePath
                }
            };
            try
            {
                p.Start();
            }
            catch (Exception ex)
            {
                Logger.Error($"{settings.Id} Run Windows Executable Command Failed wiht following excetion: {ex}");
            }                       
        }

        public override void ShowSettings()
        {
            var dlg = new RunWindowsExecutableSettingsForm((RunWindowsExecutableSettings)Settings);
            DialogResult result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                LibSettings[Id] = dlg.Settings;
                ConfiguredValue = true;
            }
        }

        public override IRemoteCommand Create(Guid id)
        {
            return new RunWindowsExecutableCommand(id);
        }
    }
}