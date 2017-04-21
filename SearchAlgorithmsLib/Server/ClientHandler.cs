using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Server {
    public class ClientHandler {
        public void HandleClient(TcpClient client) {
            new Task(() => {
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream)) {
                    string commandLine = reader.ReadLine();
                    Console.WriteLine("got command: {0}", commandLine);
                    string result = ExecuteCommand(commandLine, client);
                    writer.Write(result);
                }
                client.Close();
            }).Start();
        }
    }
}