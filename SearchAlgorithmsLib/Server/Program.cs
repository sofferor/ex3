using System;
using System.Collections.Generic;
using System.Configuration;
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

            Server server = new Server(int.Parse(ConfigurationManager.AppSettings["Port"]), ch);
            server.start();
            while (true) { }
        }
    }
}
