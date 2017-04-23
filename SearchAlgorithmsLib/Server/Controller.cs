using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace Server {
    /// <summary>
    /// Class Controller.
    /// </summary>
    /// <seealso cref="Server.IController" />
    public class Controller : IController {

        //members
        /// <summary>
        /// The commands
        /// </summary>
        private Dictionary<string, ICommand> commands;
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;
        /// <summary>
        /// The client handler
        /// </summary>
        private IClientHandler clientHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="Controller"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="clientHandler">The client handler.</param>
        /// <exception cref="System.ArgumentNullException">
        /// model
        /// or
        /// clientHandler
        /// </exception>
        public Controller(IModel model, IClientHandler clientHandler) {

            if (model == null) throw new ArgumentNullException(nameof(model));
            if (clientHandler == null) throw new ArgumentNullException(nameof(clientHandler));
            this.model = model;
            this.clientHandler = clientHandler;

            commands = new Dictionary<string, ICommand>();
            commands.Add("generate", new GenerateCommand(model));
            commands.Add("close", new CloseCommand(model));
            commands.Add("join", new JoinCommand(model));
            commands.Add("play", new PlayCommand(model));
            commands.Add("solve", new SolveCommand(model));
            commands.Add("list", new ListCommand(model));
            commands.Add("start", new StartCommand(model));

        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="commandLine">The command line.</param>
        /// <param name="client">The client.</param>
        /// <returns>System.String.</returns>
        public string ExecuteCommand(string commandLine, TcpClient client) {

            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];
            if (!commands.ContainsKey(commandKey)) {
                return "Command not found";
            }
            string[] args = arr.Skip(1).ToArray();
            ICommand command = commands[commandKey];
            return command.Execute(args, client);

        }
    }
}