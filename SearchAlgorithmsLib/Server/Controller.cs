using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Server {
    public class Controller : IController {

        //members
        private Dictionary<string, ICommand> commands;
        private IModel model;
        private IView view;

        public Controller(IModel model, IView view) {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (view == null) throw new ArgumentNullException(nameof(view));
            this.model = model;
            this.view = view;
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
            string[] args = commandLine.Split(' ');
            string commandKey = args[0];
            if (!commands.ContainsKey(commandKey)) {
                return "Command not found";
            }

        }
    }
}