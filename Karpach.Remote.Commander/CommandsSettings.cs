using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Karpach.Remote.Commander.Helpers;
using Karpach.Remote.Commander.Interfaces;
using SharpConfig;

namespace Karpach.Remote.Commander
{
    public class CommandsSettings : ICommandsSettings
    {
        private readonly Dictionary<Type,List<Guid>> _commands = new Dictionary<Type, List<Guid>>();        
        private static readonly string Path = $@"{System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\Karpach.Remote.Commander.ini";
        private readonly string _path;

        public CommandsSettings():this(Path)
        {            
        }

        public CommandsSettings(string path)
        {
            _path = path;
            var configuration = File.Exists(_path) ? Configuration.LoadFromFile(_path) : new Configuration();
            foreach (Section section in configuration)
            {
                var ids = new List<Guid>();
                foreach (Setting setting in section)
                {
                    ids.Add(setting.GetValue<Guid>());
                }
                _commands[TypeHelpers.GetType(section.Name)] = ids;
            }
        }        

        public Guid[] GetCommandIds(Type type)
        {
            return _commands.ContainsKey(type) ? _commands[type].ToArray() : null;
        }

        public void Add(Type type, Guid id)
        {            
            if (!_commands.ContainsKey(type))
            {
                _commands[type] = new List<Guid>();
            }
            if (!_commands[type].Contains(id))
            {
                _commands[type].Add(id);
            }            
            _save();
        }

        public bool Remove(Type type, Guid id)
        {            
            if (!_commands.ContainsKey(type) || !_commands[type].Contains(id))
            {
                return false;
            }
            _commands[type].Remove(id);
            _save();
            return true;
        }

        private void _save()
        {
            var configuration = new Configuration();
            foreach (KeyValuePair<Type, List<Guid>> pair in _commands)
            {
                var section = new Section(pair.Key.ToAssemblyQualifiedString());
                foreach (Guid id in pair.Value)
                {
                    section.Add("Id", id);
                }
                configuration.Add(section);
            }
            configuration.SaveToFile(_path);
        }
    }
}