using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Karpach.Remote.Commander.Interfaces;
using Karpach.Remote.Commander.Properties;
using Microsoft.Win32;
using Karpach.Remote.Commands.Interfaces;

namespace Karpach.Remote.Commander
{
    public class ControllerApplicationContext: ApplicationContext
    {        
        private readonly SettingsForm _settingsForm;
        private readonly IHostHelper _hostHelper;
        private readonly NotifyIcon _trayIcon;        
        private readonly ToolStripMenuItem _commandButton;        
        private List<IRemoteCommand> _commands;

        public ControllerApplicationContext(SettingsForm settingsForm, IHostHelper hostHelper)
        {
            var catalog = new AggregateCatalog();

            var files = Directory.GetFiles(".\\Plugins", "*.dll", SearchOption.AllDirectories);

            foreach (var dllFilePath in files)
            {
                var assembly = Assembly.LoadFrom(Path.GetFullPath(dllFilePath));
                var assemblyCatalog = new AssemblyCatalog(assembly);
                catalog.Catalogs.Add(assemblyCatalog);
            }
            
            var container = new CompositionContainer(catalog);

            _commands = new List<IRemoteCommand>(container.GetExportedValues<IRemoteCommand>());
            _commands.AddRange(container.GetExportedValues<IRemoteCommandContainer>().SelectMany(commands => commands));
            settingsForm.InitialiazeCommands(_commands.ToArray());
            
            _settingsForm = settingsForm;
            _hostHelper = hostHelper;
            var notifyContextMenu = new ContextMenuStrip();

            foreach (IRemoteCommand command in _commands)
            {
                var commandButton = new ToolStripMenuItem(command.CommandTitle)
                {
                    Image = command.TrayIcon                };
                commandButton.Click += command.RunCommand;
                notifyContextMenu.Items.Add(commandButton);
            }            

            notifyContextMenu.Items.Add("-");

            var settings = new ToolStripMenuItem("Settings")
            {
                Image = Resources.Settings.ToBitmap()
            };
            settings.Click += SettingsClick;
            notifyContextMenu.Items.Add(settings);

            notifyContextMenu.Items.Add("-");

            var exit = new ToolStripMenuItem("Exit")
            {
                Image = Resources.Exit.ToBitmap()
            };
            exit.Click += Exit;
            notifyContextMenu.Items.Add(exit);


            // Initialize Tray Icon            
            _trayIcon = new NotifyIcon
            {
                Icon = Resources.AppIcon,
                ContextMenuStrip = notifyContextMenu,
                Visible = true
            };

            _hostHelper.SecretCode = Settings.Default.SecretCode;            
            _hostHelper.CreateHostAsync(Settings.Default.RemotePort);
        }

        private void SettingsClick(object sender, EventArgs e)
        {            
            if (_settingsForm.ShowDialog() == DialogResult.OK)
            {
                if (Settings.Default.RemotePort != _settingsForm.Port)
                {
                    _hostHelper.CreateHostAsync(_settingsForm.Port);
                }
                if (Settings.Default.AutoStart != _settingsForm.AutoStart)
                {
                    SetAutoStart(_settingsForm.AutoStart);
                }                
                Settings.Default.AutoStart = _settingsForm.AutoStart;                
                Settings.Default.RemotePort = _settingsForm.Port;
                Settings.Default.SecretCode = _settingsForm.SecretCode;                
                Settings.Default.Save();
                // Update host helper
                _hostHelper.SecretCode = Settings.Default.SecretCode;                
            }
        }

        private void SetAutoStart(bool autoStart)
        {
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (autoStart)
            {
                // Add the value in the registry so that the application runs at startup
                rkApp?.SetValue("Karpach.RemoteShutdown", Application.ExecutablePath);
            }
            else
            {
                rkApp?.DeleteValue("Karpach.RemoteShutdown", false);
            }
        }        

        void Exit(object sender, EventArgs e)
        {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            _trayIcon.Visible = false;            
            _hostHelper.Cancel();
            Application.Exit();
        }        
    }
}