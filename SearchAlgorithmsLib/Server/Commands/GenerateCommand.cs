using MazeLib;
using System.Net.Sockets;

namespace Server {
    /// <summary>
    /// Class GenerateCommand.
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    public class GenerateCommand : ICommand {

        //members
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;

        //constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public GenerateCommand(IModel model) {
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

            SearchableMazeAdapter searchableMaze = model.GenerateMaze(name, rows, cols);
            return searchableMaze.MyMaze.ToJSON();
        }
    }
}