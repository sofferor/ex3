using System.Net.Sockets;

namespace Server {
    public interface IClientHandler {

        void SetController(IController c);

        void HandleClient(TcpClient client);
    }
}