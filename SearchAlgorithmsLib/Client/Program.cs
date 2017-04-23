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
    /// <summary>
    /// Class Program.
    /// </summary>
    class Program {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args) {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), int.Parse(ConfigurationManager.AppSettings["Port"]));
            bool stop = false;


            while (true) {
                TcpClient client = new TcpClient();
                client.Connect(ep);
                NetworkStream stream = client.GetStream();
                BinaryWriter writer = new BinaryWriter(stream);
                BinaryReader reader = new BinaryReader(stream);
                // Send data to server
                
                string commandLine = Console.ReadLine();
                

                writer.Write(commandLine);
                writer.Flush();

                string result = reader.ReadString();
                Console.WriteLine("{0}", result);

                //if multipile command
                if (commandLine.Contains("start") || commandLine.Contains("join") || commandLine.Contains("play")) {
                    stop = false;
                    
                    new Task(() => {
                        while (true) {
                            try {
                                reader = new BinaryReader(stream);
                                string flow = reader.ReadString();
                                if (flow.Contains("wait")) {
                                    continue;
                                }
                                Console.WriteLine("{0}", flow);
                                if (stop || flow.Contains("close")) {
                                    stop = true;
                                    break;
                                }
                            } catch (Exception e) {
                                client = new TcpClient();
                                client.Connect(ep);
                                stream = client.GetStream();
                                writer = new BinaryWriter(stream);
                                reader = new BinaryReader(stream);
                            }
                        }
                    }).Start();

                    new Task(() => {
                        while (true) {
                            try {
                                string flowToServer = Console.ReadLine();
                                writer.Write(flowToServer);
                                writer.Flush();
                                //Console.WriteLine("{0}", flowToServer);
                                if (stop || flowToServer.Contains("close")) {
                                    stop = true;
                                    break;
                                }
                            } catch (Exception e) {
                                client = new TcpClient();
                                client.Connect(ep);
                                stream = client.GetStream();
                                writer = new BinaryWriter(stream);
                                reader = new BinaryReader(stream);
                            }
                        }
                    }).Start();

                    while (!stop) { }
                }
                

            }
        }
    }
}



