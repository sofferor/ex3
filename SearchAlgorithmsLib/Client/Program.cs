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
            bool stop = false;


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
                    stop = false;
                    
                    new Task(() => {
                        while (true) {
                            try {
                                string flow = reader.ReadString();
                                if (flow.Equals("wait")) {
                                    continue;
                                }
                                Console.WriteLine("Result = {0}", result);
                                if (flow.Equals("close")) {
                                    stop = true;
                                    break;
                                }
                            } catch (Exception e) {
                                //client = new TcpClient();
                                //client.Connect(ep);
                                //stream = client.GetStream();
                                //writer = new BinaryWriter(stream);
                                //reader = new BinaryReader(stream);
                            }
                        }
                    }).Start();

                    new Task(() => {
                        while (true) {
                            try {
                                string flowToServer = Console.ReadLine();
                                writer.Write(flowToServer);
                                Console.WriteLine("Result = {0}", result);
                                if (stop || flowToServer.Equals("close")) {
                                    break;
                                }
                            } catch (Exception e) {
                                //client = new TcpClient();
                                //client.Connect(ep);
                                //stream = client.GetStream();
                                //writer = new BinaryWriter(stream);
                                //reader = new BinaryReader(stream);
                            }
                        }
                    }).Start();

                    while (!stop) { }
                }
                

            }
        }
    }
}



