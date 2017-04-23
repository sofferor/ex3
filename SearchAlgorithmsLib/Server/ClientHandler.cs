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
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream);
                
                string commandLine = reader.ReadLine();
                Console.WriteLine("got command: {0}", commandLine);
                string result = controller.ExecuteCommand(commandLine, client);
                writer.Write(result);
                writer.Flush();
                
                client.Close();
            }).Start();
        }

    }
}