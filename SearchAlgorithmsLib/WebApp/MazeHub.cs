using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using WebApp.Models;

namespace WebApp {
    public class MazeHub : Hub {
        static Model model = new Model();
        static Dictionary<string, string> clients = new Dictionary<string, string>();
        public void Hello() {
            Clients.All.hello();
        }

        public void Send(string name, string message) {
            Clients.All.broadcastMessage(name, message);
        }
    }
}