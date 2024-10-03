using AnalyzerManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

using AnalyzerManager;

namespace DataExchangeModule
{
    public class DllLoader : IDllLoader
    {
        static bool IsDLLFile(string path)
        {
            return Path.GetExtension(path).Equals(".dll", StringComparison.OrdinalIgnoreCase);
        }
        public Dictionary<string, List<string>> LoadToolsFromFolder(string folder)
        {
            Dictionary<string, List<string>> hashMap = new Dictionary<string, List<string>>();

            try
            {
                string[] files = Directory.GetFiles(folder);

                foreach (string file in files)
                {
                    if (File.Exists(file) && IsDLLFile(file))
                    {
                        Assembly fileAssembly = Assembly.LoadFile(file);

                        var targetFrameworkAttribute = fileAssembly.GetCustomAttribute<TargetFrameworkAttribute>();
                        if (targetFrameworkAttribute != null && targetFrameworkAttribute.FrameworkName == ".NETCoreApp,Version=v8.0")
                        {
                            try
                            {
                                Assembly assembly = Assembly.LoadFrom(file);
                                Console.WriteLine($"Assembly: {assembly.FullName}");
                                Console.WriteLine("=========================");

                                Type analyzerInterface = typeof(IAnalyzer);
                                Type toolInterface = typeof(ITool);
                                Type[] types = assembly.GetTypes();

                                foreach (Type type in types)
                                {
                                    if (analyzerInterface.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                                    {
                                        Console.WriteLine(type.FullName);
                                        MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

                                        // Attempt to create an instance of the type that implements ITool
                                        try
                                        {
                                            object instance = Activator.CreateInstance(type);
                                            Console.WriteLine($"Instance of {type.FullName} created successfully!");

                                            // Retrieve the property values from the ITool interface
                                            PropertyInfo[] properties = toolInterface.GetProperties();
                                            foreach (PropertyInfo property in properties)
                                            {
                                                if (property.CanRead)  // Ensure the property is readable
                                                {
                                                    object value = property.GetValue(instance);
                                                    if (hashMap.ContainsKey($"{property.Name}"))
                                                    {
                                                        // Add a new value to the existing list associated with "Key1"
                                                        hashMap[$"{property.Name}"].Add($"{value}");
                                                    }
                                                    else
                                                    {
                                                        // If the key doesn't exist, you can add a new key with a new list
                                                        hashMap[$"{property.Name}"] = new List<string> { $"{value}" };
                                                    }

                                                    Console.WriteLine($"{property.Name} = {value}");
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine($"Failed to create an instance of {type.FullName}: {ex.Message}");
                                        }
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Error while processing {file}: {e.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Invalid Target Framework for Assembly {fileAssembly.GetName()}.");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in Main: {e.Message}");
            }


            
            return hashMap;
        }

        public string ResolveDllNamingConflict(string dllName)
        {
            throw new NotImplementedException();
        }
    }
}
