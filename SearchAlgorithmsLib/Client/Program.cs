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
<<<<<<< HEAD

            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5555);
            
            while (true) {
=======
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
>>>>>>> cd68e5fcf0cbc991f6dd2b8fba3d49399544a726

                TcpClient client = new TcpClient();

                // Send data to server
                Console.Write("Please enter a command: ");
                string commandLine = Console.ReadLine();

<<<<<<< HEAD

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
=======
                try {
                    writer.Write(commandLine);
                    writer.Flush();
                }
                catch (Exception e) {
                    client.Connect(ep);
>>>>>>> cd68e5fcf0cbc991f6dd2b8fba3d49399544a726
                }

                string result = reader.ReadString();
                Console.WriteLine("Result = {0}", result);

            }
        }
    }
}
