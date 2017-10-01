using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SharpConfig;

namespace Karpach.Remote.Commands
{
    public class LibSettings
    {
        public readonly List<WakeOnLanCommandSettings> WakeOnLanCommandSettings = new List<WakeOnLanCommandSettings>();
        private readonly Configuration _configuration;
        private readonly string _path = $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\Karpach.Remote.Commands.ini";

        public LibSettings()
        {
            _configuration = File.Exists(_path) ? Configuration.LoadFromFile(_path) : new Configuration();
            foreach (Section section in _configuration)
            {                
                if (section.Name.StartsWith("WakeOnLanCommandSettings"))
                {
                    WakeOnLanCommandSettings.Add(section.ToObject<WakeOnLanCommandSettings>());
                }
            }
        }

        public void Save()
        {
            bool[] updated = new bool[WakeOnLanCommandSettings.Count];
            foreach (Section section in _configuration)
            {
                if (section.Name.StartsWith("WakeOnLanCommandSettings"))
                {
                    Guid id = section["Id"].GetValue<Guid>();
                    int index = WakeOnLanCommandSettings.FindIndex(s => s.Id == id);                    
                    if (index >= 0)
                    {                               
                        section.GetValuesFrom(WakeOnLanCommandSettings[index]);
                        updated[index] = true;
                    }                    
                }
            }
            foreach (int index in updated.Where(u=>!u).Select((u,i)=>i))
            {                                
                _configuration.Add(Section.FromObject($"WakeOnLanCommandSettings{index}", WakeOnLanCommandSettings[index]));
            }
            _configuration.SaveToFile(_path);
        }
    }
}
