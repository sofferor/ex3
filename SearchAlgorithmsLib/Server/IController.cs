using System.Net.Sockets;

namespace Server {
    /// <summary>
    /// Interface IController
    /// </summary>
    public interface IController {
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="commandLine">The command line.</param>
        /// <param name="client">The client.</param>
        /// <returns>System.String.</returns>
        string ExecuteCommand(string commandLine, TcpClient client);
    }
}

