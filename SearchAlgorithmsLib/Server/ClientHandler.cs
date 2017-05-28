using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Server {
    /// <summary>
    /// Class ClientHandler.
    /// </summary>
    /// <seealso cref="Server.IClientHandler" />
    public class ClientHandler : IClientHandler {

        //members
        /// <summary>
        /// The controller
        /// </summary>
        private IController controller;

        /// <summary>
        /// Sets the controller.
        /// </summary>
        /// <param name="c">The c.</param>
        public void SetController(IController c) {
            controller = c;
        }

        /// <summary>
        /// Handles the client.
        /// </summary>
        /// <param name="client">The client.</param>
        public void HandleClient(TcpClient client) {
            new Task(() => {
                NetworkStream stream = client.GetStream();
                BinaryReader reader = new BinaryReader(stream);
                BinaryWriter writer = new BinaryWriter(stream);

                string commandLine;
                do {
                    try {
                        commandLine = reader.ReadString();
                        //Console.WriteLine("got command: {0}", commandLine);
                        string result = controller.ExecuteCommand(commandLine, client);
                        writer.Write(result);
                        writer.Flush();
                    } catch (Exception e) {
                        commandLine = "exit";
                    }
                } while (commandLine.Contains("start") || commandLine.Contains("play") || commandLine.Contains("join"));
                
                client.Close();
            }).Start();
        }

    }
}