using System;
using System.Net.Sockets;
using MazeLib;
using SearchAlgorithmsLib;

namespace Server {
    /// <summary>
    /// Class SolveCommand.
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    public class SolveCommand : ICommand {

        //members
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;

        //constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SolveCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public SolveCommand(IModel model) {
            this.model = model;
        }

        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.InvalidCastException"></exception>
        public string Execute(string[] args, TcpClient client = null) {

            string name = args[0];
            Algoritem algoritem;
            try {
                algoritem = (Algoritem)int.Parse(args[1]);
            } catch {
                throw new InvalidCastException();
            }

            Solution<Position> solution = model.Solve(name, algoritem);
            return solution.ToJson();
        }
    }
}