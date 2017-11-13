using System;
using System.Drawing;
using System.Reflection;
using Karpach.Remote.Commands.Interfaces;

namespace Karpach.Remote.Commands.Base
{
    public abstract class CommandBase: IRemoteCommand
    {
        protected const string NotConfigured = "Not Configured";
        protected readonly LibSettings LibSettings;
        protected bool ConfiguredValue;
        public Guid Id => Settings.Id;
        public abstract string CommandTitle { get; }
        protected readonly CommandSettingsBase Settings;
        public bool Configured => ConfiguredValue;
        public abstract Image TrayIcon { get; }        
        public string AssemblyName => Assembly.GetExecutingAssembly().GetName().Name;
        public abstract void RunCommand(params object[] parameters);
        public abstract void ShowSettings();
        public abstract IRemoteCommand Create(Guid id);
        protected virtual Type SettingsType => typeof(CommandSettingsBase);

        protected CommandBase(Guid? id)
        {
            LibSettings = new LibSettings(Assembly.GetAssembly(SettingsType));
            if (id.HasValue)
            {                
                CommandSettingsBase settings = LibSettings[id.Value] as CommandSettingsBase;
                ConfiguredValue = settings != null;
                if (ConfiguredValue)                
                {
                    Settings = settings;
                }
                else
                {                    
                    Settings = (CommandSettingsBase) Activator.CreateInstance(SettingsType);
                    Settings.Id = id.Value;                    
                }
            }
            else
            {
                CommandSettingsBase[] allSettings = LibSettings.GetValues(SettingsType);
                ConfiguredValue = allSettings.Length > 0;
                if (ConfiguredValue)
                {
                    Settings = allSettings[0];                    
                }
                else                
                {
                    Settings = (CommandSettingsBase)Activator.CreateInstance(SettingsType);
                    Settings.Id = Guid.NewGuid();
                }
            }
        }

        public bool Delete()
        {
            return LibSettings.Remove(Id);
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