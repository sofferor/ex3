using System;
using System.Net.Sockets;

namespace Server {
    public class JoinCommand : ICommand {

        //members
        private IModel model;

        //constructor
        public JoinCommand(IModel model) {
            if (model == null) throw new ArgumentNullException(nameof(model));
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client = null) {

            string name = args[0];

            SearchableMazeAdapter searchableMaze = model.Join(name, client);
            return searchableMaze.MyMaze.ToJSON();
        }
    }
}