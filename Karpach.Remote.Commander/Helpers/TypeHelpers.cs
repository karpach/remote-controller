using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Karpach.Remote.Commander.Helpers
{
    public static class TypeHelpers
    {
        public static string ToAssemblyQualifiedString(this Type type)
        {            
            return $"{type},{type.Assembly.GetName().Name}";
        }

        public static Type GetType(string s)
        {
            string[] parts = s.Split(',');
            if (parts.Length == 2)
            {
                string initialPath = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
                string dllName = $"{parts[1]}.dll";
                string dllPath = FindFileRecursive(initialPath, dllName);
                if (dllPath == null)
                {
                    return Type.GetType(s);
                }
                Assembly assembly = Assembly.LoadFrom(dllPath);                  
                Type result = assembly.GetType(parts[0],true);                
                return result;
            }
            return Type.GetType(s);            
        }

        private static string FindFileRecursive(string path, string fileName)
        {
            string filePath = Directory.GetFiles(path, fileName).FirstOrDefault(f => f.EndsWith($"\\{fileName}"));
            if (!string.IsNullOrEmpty(filePath))
            {
                return filePath;
            }
            string[] directories = Directory.GetDirectories(path);
            foreach (string directory in directories)
            {
                string newPath = Path.Combine(path, directory);
                string dllPath = FindFileRecursive(newPath, fileName);
                if (!string.IsNullOrEmpty(dllPath))
                {
                    return dllPath;
                }
            }
            return null;
        }
    }
}