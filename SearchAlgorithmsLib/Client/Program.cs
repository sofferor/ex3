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
            TcpClient client = new TcpClient();
            client.Connect(ep);
            Console.WriteLine("You are connected");
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);

            new Task(() => {
                while (true) {
                    // Send data to server
                    string commandLine = Console.ReadLine();
                    Console.Write("Please enter a command: ");
                    if (!client.Connected) {
                        client.Connect(ep);
                    }
                    writer.Write(commandLine);
                    writer.Flush();
                }
            }).Start();

            new Task(() => {
                while (true) {
                    // Get result from server
                    string result = reader.ReadLine();
                    Console.WriteLine("Result = {0}", result);
                }
            }).Start();

            while (true) {
                
            }
        }
    }
}
