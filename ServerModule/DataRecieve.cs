using System.Reflection;
using System.Runtime.Versioning;

using NetworkingModule;

namespace ServerModule
{
    public class DataRecieve
    {
        public static void RecieveDLLFromClient()
        {
            string serverIP = "127.0.0.1";
            int port = 5000;

            string folderPath = @"C:\received";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            try
            {
                Networking network = new Networking(serverIP, port);

                network.RecieveFiles(folderPath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error recieving files: {ex.Message}");
            }
            

        }

    }
}
