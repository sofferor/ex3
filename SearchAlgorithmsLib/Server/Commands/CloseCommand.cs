using System.Net.Sockets;

namespace Server {
    public class CloseCommand : ICommand {
        //members
        private IModel model;

        public CloseCommand(IModel model) {
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client = null) {

            string name = args[0];
            
            return model.Close(name, client);
        }
    }
}