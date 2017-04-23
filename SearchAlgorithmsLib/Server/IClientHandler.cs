using System.Net.Sockets;

namespace Server {
    /// <summary>
    /// Interface IClientHandler
    /// </summary>
    public interface IClientHandler {

        /// <summary>
        /// Sets the controller.
        /// </summary>
        /// <param name="c">The c.</param>
        void SetController(IController c);

        /// <summary>
        /// Handles the client.
        /// </summary>
        /// <param name="client">The client.</param>
        void HandleClient(TcpClient client);
    }
}