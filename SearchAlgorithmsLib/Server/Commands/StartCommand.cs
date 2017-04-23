using System;
using System.Net.Sockets;

namespace Server {
    /// <summary>
    /// Class StartCommand.
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    public class StartCommand : ICommand {

        //members
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;

        //constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="StartCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <exception cref="System.ArgumentNullException">model</exception>
        public StartCommand(IModel model) {
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
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);

            SearchableMazeAdapter searchableMaze = model.Start(name, rows, cols, client);
            return searchableMaze.MyMaze.ToJSON();
        }
    }
}