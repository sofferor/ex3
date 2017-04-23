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
            
            

            new Task(() => {
                while (true) {
                    // Send data to server
                    Console.Write("Please enter a command: ");
                    string commandLine = Console.ReadLine();
                    

                    //connect to server
                    while (!client.Connected) {
                        try {
                            client.Connect(ep);
                        } catch { }
                    }

                    NetworkStream stream = client.GetStream();
                    StreamWriter writer = new StreamWriter(stream);

                    writer.WriteLine(commandLine);
                    writer.Flush();
                }
            }).Start();

            new Task(() => {
                while (true) {

                    //wait to connect
                    while (!client.Connected) {
                        System.Threading.Thread.Sleep(5000);
                    }

                    NetworkStream stream = client.GetStream();
                    StreamReader reader = new StreamReader(stream);

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
