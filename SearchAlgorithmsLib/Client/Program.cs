using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client {
    class Program {
        static void Main(string[] args) {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5555);            
            
            while (true) {
                TcpClient client = new TcpClient();
                client.Connect(ep);
                NetworkStream stream = client.GetStream();
                BinaryWriter writer = new BinaryWriter(stream);
                BinaryReader reader = new BinaryReader(stream);
                // Send data to server
                Console.WriteLine("Please enter a command: ");
                string commandLine = Console.ReadLine();

                do {
                    try {
                        writer.Write(commandLine);
                        writer.Flush();
                    }
                    catch (Exception e) {
                        client.Connect(ep);
                    }

                    string result = reader.ReadString();

                    Console.WriteLine("Result = {0}", result);
                } while (commandLine.Equals("start") || commandLine.Equals("join") || commandLine.Equals("play"));

            }
        }
    }
}
