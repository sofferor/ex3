using System.Net.Sockets;

namespace Server {
    public interface IController {
        string ExecuteCommand(string commandLine, TcpClient client);
    }
}

