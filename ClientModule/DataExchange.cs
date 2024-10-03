using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using NetworkingModule;

namespace ClientModule
{
    public class DataExchange
    {
        public static bool IsDLLFile(string filePath)
        {
            return Path.GetExtension(filePath).Equals(".dll", StringComparison.OrdinalIgnoreCase);
        }

        public static void UploadDLLFromClientToServer()
        {
            string folder = @"C:\temp";
            string serverIP = "127.0.0.1";
            int port = 5000;

            if (!Directory.Exists(folder))
            {
                System.Diagnostics.Trace.WriteLine($"The folder {folder} does not exist.");
                throw new Exception("The target folder {folder} does not exist.");
            }
            string[] files = Directory.GetFiles(folder);
      
            Networking network = new Networking(serverIP, port);

            foreach (string file in files)
            {
                if (File.Exists(file) && IsDLLFile(file))
                {
                    try
                    {
                        Assembly assembly = Assembly.LoadFile(file);
                        var targetFrameworkAttribute = assembly.GetCustomAttribute<TargetFrameworkAttribute>();

                        if (targetFrameworkAttribute != null && targetFrameworkAttribute.FrameworkName == ".NETCoreApp,Version=v8.0")
                        {
                            string result = network.UploadFile(file);
                            System.Diagnostics.Trace.WriteLine(result);
                        }
                        else
                        {
                            System.Diagnostics.Trace.WriteLine($"Invalid Target Framework for Assembly {assembly.GetName()}.");
                        }
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Trace.WriteLine($"Error processing file {file}: {e.Message}");
                        throw new Exception(e.Message);
                    }
                }
                System.Diagnostics.Trace.WriteLine("All valid files processed.");

            }
            
        }
    }
}
