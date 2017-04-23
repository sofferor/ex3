using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server {
    class Program {
        static void Main(string[] args) {

            IClientHandler ch = new ClientHandler();
            IModel model = new Model();
            IController controller = new Controller(model, ch);

            ch.SetController(controller);
            model.SetController(controller);

            Server server = new Server(5555, ch);
            server.start();
            while (true) { }
        }
    }
}
