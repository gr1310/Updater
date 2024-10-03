using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;

namespace NetworkingModule
{
    public class Networking
    {
        private string _serverIP;
        private int _port;
        private TcpListener _server;

        public Networking(string serverIP, int port)
        {
            _serverIP = serverIP;
            _port = port;
            _server = new TcpListener(IPAddress.Any, _port);
        }

        public string UploadFile(string filePath)
        {
            try
            {
                using (TcpClient client = new TcpClient(_serverIP, _port))
                {
                    System.Diagnostics.Trace.WriteLine("Connected to the server.");
                    using (NetworkStream stream = client.GetStream())
                    {
                        using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                        {
                            byte[] buffer = new byte[1024];
                            int bytesRead;
                            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                stream.Write(buffer, 0, bytesRead);
                            }
                        }
                    }
                }
                return $"Successfully sent: {filePath}";
            }
            catch (Exception e)
            {
                throw new Exception($"Error processing file {filePath}: {e.Message}");
            }
        }

        public string RecieveFiles(string folderPath)
        {
            _server.Start();
            System.Diagnostics.Trace.WriteLine("Server started. Waiting for connection...");

            using (TcpClient client = _server.AcceptTcpClient())
            {
                System.Diagnostics.Trace.WriteLine("Client connected!");

                using (NetworkStream stream = client.GetStream())
                {
                    byte[] buffer = new byte[1024];
                    try
                    {
                        while (true)
                        {
                            // Read file name length
                            byte[] fileNameLengthBuffer = new byte[4];
                            int bytesRead = stream.Read(fileNameLengthBuffer, 0, 4);
                            if (bytesRead == 0) break; // No more files

                            int fileNameLength = BitConverter.ToInt32(fileNameLengthBuffer, 0);

                            // Read file name
                            byte[] fileNameBuffer = new byte[fileNameLength];
                            stream.Read(fileNameBuffer, 0, fileNameLength);

                            string fileName = Encoding.UTF8.GetString(fileNameBuffer);
                            string destinationFilePath = Path.Combine(folderPath, fileName);

                            // Read file size
                            byte[] fileSizeBuffer = new byte[8]; // long is 8 bytes
                            stream.Read(fileSizeBuffer, 0, fileSizeBuffer.Length);
                            long fileSize = BitConverter.ToInt64(fileSizeBuffer, 0);

                            // Read file content based on file size
                            using (FileStream fileStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write))
                            {
                                long totalBytesRead = 0;
                                while (totalBytesRead < fileSize)
                                {
                                    int bytesToRead = (int)Math.Min(buffer.Length, fileSize - totalBytesRead);
                                    bytesRead = stream.Read(buffer, 0, bytesToRead);
                                    if (bytesRead == 0) break;

                                    fileStream.Write(buffer, 0, bytesRead);
                                    totalBytesRead += bytesRead;
                                }
                            }

                            Console.WriteLine($"File received and saved as '{destinationFilePath}'");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error receiving files: {ex.Message}");
                        throw new Exception(ex.Message);
                    }
                }
            }

            _server.Stop();
            return "Successfully received";
        }

    }
}

