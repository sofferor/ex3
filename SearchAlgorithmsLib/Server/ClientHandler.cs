using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Server {
    public class ClientHandler : IClientHandler {

        //members
        private IController controller;

        public void SetController(IController c) {
            controller = c;
        }

        public void HandleClient(TcpClient client) {
            new Task(() => {
                NetworkStream stream = client.GetStream();
                BinaryReader reader = new BinaryReader(stream);
                BinaryWriter writer = new BinaryWriter(stream);

                string commandLine;
                do {
                    commandLine = reader.ReadString();
                    Console.WriteLine("got command: {0}", commandLine);
                    string result = controller.ExecuteCommand(commandLine, client);
                    writer.Write(result);
                    writer.Flush();
                } while (commandLine)


                client.Close();
            }).Start();
        }

    }
}