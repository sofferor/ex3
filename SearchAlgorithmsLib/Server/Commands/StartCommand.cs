using System;
using System.Net.Sockets;

namespace Server {
    public class StartCommand : ICommand {

        //members
        private IModel model;

        //constructor
        public StartCommand(IModel model) {
            if (model == null) throw new ArgumentNullException(nameof(model));
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client = null) {

            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);

            SearchableMazeAdapter searchableMaze = model.Start(name, rows, cols, client);
            return searchableMaze.MyMaze.ToJSON();
        }
    }
}