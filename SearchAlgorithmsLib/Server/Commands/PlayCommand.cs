using Newtonsoft.Json.Linq;
using System;
using System.Net.Sockets;

namespace Server {
    public class PlayCommand : ICommand {

        //members
        private IModel model;

        //constructor
        public PlayCommand(IModel model) {
            if (model == null) throw new ArgumentNullException(nameof(model));
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client = null) {

            string step = args[0];
            model.Play(step, client);
            return new JObject().ToString();
        }
    }
}