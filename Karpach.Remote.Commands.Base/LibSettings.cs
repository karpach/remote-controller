using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SharpConfig;

namespace Karpach.Remote.Commands.Base
{
	public class LibSettings
	{
		private readonly string _path;

		private readonly Dictionary<Guid,object> _objects = new Dictionary<Guid, object>();        

		public LibSettings(Assembly assembly)
		{
			_path = $@"{Path.GetDirectoryName(assembly.Location)}\{assembly.GetName().Name}.ini";
			Configuration configuration = File.Exists(_path) ? Configuration.LoadFromFile(_path) : new Configuration();            
			foreach (Section section in configuration)
			{
				if (string.Equals(section.Name, "$SharpConfigDefaultSection", StringComparison.InvariantCultureIgnoreCase))
				{
					continue;
				}
				Type type = assembly.GetType(section.Name, false);
				if (type == null)
				{
					throw new Exception($"Unable to load type: {section.Name}");
				}
				_objects[section["Id"].GetValue<Guid>()] = section.ToObject(type);                
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
			
		public CommandSettingsBase[] GetValues(Type type)
		{
			return _objects.Values.Where(v => v.GetType() == type).Cast<CommandSettingsBase>().ToArray();
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
