using System;
using System.Net.Sockets;

namespace Server {
    /// <summary>
    /// Class JoinCommand.
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    public class JoinCommand : ICommand {

        //members
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;

        //constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="JoinCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <exception cref="System.ArgumentNullException">model</exception>
        public JoinCommand(IModel model) {
            if (model == null) throw new ArgumentNullException(nameof(model));
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

            SearchableMazeAdapter searchableMaze = model.Join(name, client);
            return searchableMaze.MyMaze.ToJSON();
        }
    }
}