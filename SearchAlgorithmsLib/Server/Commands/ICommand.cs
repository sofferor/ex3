using System.Net.Sockets;

namespace Server {
    public interface ICommand {
        string Execute(string[] args, TcpClient client = null);
    }
}