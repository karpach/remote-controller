using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Karpach.Remote.Commander.Helpers;
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
        private readonly CommandsManager _commandsManager;

        public ControllerApplicationContext(SettingsForm.Factory settingsFormFactory, HostHelper.Factory hostHelperFactory, ICommandsSettings commandsSettings, CommandsManager.Factory commandManagerFactory)
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

            var commands = new List<IRemoteCommand>(container.GetExportedValues<IRemoteCommand>());
            commands.AddRange(container.GetExportedValues<IRemoteCommandContainer>().SelectMany(c => c));            
            _commandsManager = commandManagerFactory.Invoke(commands, commandsSettings);                        
            _settingsForm = settingsFormFactory.Invoke(_commandsManager);
            _hostHelper = hostHelperFactory.Invoke(_commandsManager);                        

            // Initialize Tray Icon            
            _trayIcon = new NotifyIcon
            {
                Icon = Resources.AppIcon,
                ContextMenuStrip = GetContextMenuStrip(),
                Visible = true
            };

            _hostHelper.SecretCode = Settings.Default.SecretCode;            
            _hostHelper.CreateHostAsync(Settings.Default.RemotePort);
        }

        private ContextMenuStrip GetContextMenuStrip()
        {
            var notifyContextMenu = new ContextMenuStrip();
            bool hasConfiguredCommands = false;
            foreach (IRemoteCommand command in _commandsManager)
            {
                if (command.Configured)
                {
                    var commandButton = new ToolStripMenuItem(command.CommandTitle)
                    {
                        Image = command.TrayIcon
                    };
                    commandButton.Click += command.RunCommand;
                    notifyContextMenu.Items.Add(commandButton);
                    hasConfiguredCommands = true;
                }                
            }
            if (hasConfiguredCommands)
            {
                notifyContextMenu.Items.Add("-");
            }            

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

            return notifyContextMenu;
        }

        private void SettingsClick(object sender, EventArgs e)
        {            
            if (_settingsForm.ShowDialog() == DialogResult.OK)
            {
                _trayIcon.ContextMenuStrip = GetContextMenuStrip();
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