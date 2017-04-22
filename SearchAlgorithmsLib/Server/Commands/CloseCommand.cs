using System.Net.Sockets;

namespace Server {
    public class CloseCommand : ICommand {
        //members
        private IModel model;

        public CloseCommand(IModel model) {
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client = null) {
            throw new System.NotImplementedException();
        }
    }
}