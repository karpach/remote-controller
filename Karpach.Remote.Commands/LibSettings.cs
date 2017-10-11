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
        private static readonly string DefaultPath = $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\Karpach.Remote.Commands.ini";
        private readonly string _path;

        private readonly Dictionary<Guid,object> _objects = new Dictionary<Guid, object>();

        public LibSettings():this(DefaultPath)
        {
        }

        public LibSettings(string path)
        {
            _path = path;
            Configuration configuration = File.Exists(_path) ? Configuration.LoadFromFile(_path) : new Configuration();            
            foreach (Section section in configuration)
            {
                _objects[section["Id"].GetValue<Guid>()] = section.ToObject(Type.GetType(section.Name));                
            }
        }

        public object this[Guid id]
        {
            get => _objects.ContainsKey(id) ? _objects[id] : null;
            set
            {
                _objects[id] = value; 
                _save();
            }
        }

        public bool Remove(Guid id)
        {
            if (!_objects.ContainsKey(id))
            {
                return false;
            }
            bool result = _objects.Remove(id);
            _save();
            return result;
        }

        public int Length => _objects.Keys.Count;
            
        public T[] GetValues<T>()
        {
            return _objects.Values.OfType<T>().ToArray();
        }


        private void _save()
        {
            var configuration = new Configuration();            
            foreach (object obj in _objects.Values)
            {                                
                configuration.Add(Section.FromObject(obj.GetType().ToString(), obj));
            }
            configuration.SaveToFile(_path);
        }
    }
}
