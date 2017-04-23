using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Client {
    class Program {
        static void Main(string[] args) {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), int.Parse(ConfigurationManager.AppSettings["Port"]));            
            
            while (true) {
                TcpClient client = new TcpClient();
                client.Connect(ep);
                NetworkStream stream = client.GetStream();
                BinaryWriter writer = new BinaryWriter(stream);
                BinaryReader reader = new BinaryReader(stream);
                // Send data to server
                Console.WriteLine("Please enter a command: ");
                string commandLine = Console.ReadLine();



                writer.Write(commandLine);
                writer.Flush();

                string result = reader.ReadString();
                Console.WriteLine("Result = {0}", result);

                //if multipile command
                if (commandLine.Equals("start") || commandLine.Equals("join") || commandLine.Equals("play")) {
                    new Task(() => {
                        while (true) {
                            try {
                                string result = reader.ReadString();
                                Console.WriteLine("Result = {0}", result);
                            } catch (Exception e) {
                                client = new TcpClient();
                                client.Connect(ep);
                                stream = client.GetStream();
                                writer = new BinaryWriter(stream);
                                reader = new BinaryReader(stream);
                            }
                        }
                    }).Start();
                }
                

            }
        }
    }
}






new Task(() => {
    while (true) {
        try {
            string result = reader.ReadString();
            Console.WriteLine("Result = {0}", result);
        } catch (Exception e) {
            client = new TcpClient();
            client.Connect(ep);
            stream = client.GetStream();
            writer = new BinaryWriter(stream);
            reader = new BinaryReader(stream);
        }
    }
}).Start();




