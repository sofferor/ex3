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

                // Send data to server
                Console.Write("Please enter a command: ");
                string commandLine = Console.ReadLine();


                //connect to server
                while (!client.Connected) {
                    try {
                        client.Connect(ep);
                    }
                    catch { }
                }

                NetworkStream stream = client.GetStream();
                BinaryWriter writer = new BinaryWriter(stream);

                writer.Write(commandLine);
                writer.Flush();
            }
            

            Task receive = new Task(() => {
                while (true) {

                    //wait to connect
                    while (!client.Connected) {
                        System.Threading.Thread.Sleep(10);
                    }

                    NetworkStream stream = client.GetStream();
                    BinaryReader reader = new BinaryReader(stream);

                    // Get result from server
                    string result = reader.ReadString();
                    Console.WriteLine("Result = {0}", result);
                }
            });
            receive.Start();

            send.Wait();
            receive.Wait();
        }
    }
}
