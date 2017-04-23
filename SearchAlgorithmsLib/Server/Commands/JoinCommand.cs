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
            return model.GamesList();
        }
    }
}