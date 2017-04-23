using System.Net.Sockets;

namespace Server {
    /// <summary>
    /// Class CloseCommand.
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    public class CloseCommand : ICommand {
        //members
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="CloseCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public CloseCommand(IModel model) {
            this.model = model;
        }

        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns>System.String.</returns>
        public string Execute(string[] args, TcpClient client = null) {

            string name = args[0];
            
            return model.Close(name, client);
        }
    }
}