using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace Server {
    public class Controller : IController {

        //members
        private Dictionary<string, ICommand> commands;
        private IModel model;
        private IClientHandler clientHandler;
        
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