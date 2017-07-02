using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.EntitySql;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using WebApp.Models;
using Newtonsoft.Json.Linq;

namespace WebApp {
    public class MazeHub : Hub {
        static Model model = new Model();
        private static ConcurrentDictionary<string, List<string>> connectedUsers =
            new ConcurrentDictionary<string, List<string>>();

        public void Connect(string mazeName) {
            connectedUsers[mazeName][0] = Context.ConnectionId;
        }

        public void Hello() {
            Clients.All.hello();
        }

        public void Send(string name, string message) {
            Clients.All.broadcastMessage(name, message);
        }

        public void StartGame(string mazeName, int rows, int col) {
            model.Start(mazeName, rows, col, connectedUsers[mazeName][0]);
        }

        public void JoinGame(string mazeName) {
            model.Join(mazeName, Context.ConnectionId);
            connectedUsers[mazeName][1] = Context.ConnectionId;

            //send the maze to the two clients.
            JObject jMaze = JObject.Parse(model.GetMazeByName(mazeName).MyMaze.ToJSON());
            Clients.Client(connectedUsers[mazeName][0]).StartGameFromJoin(jMaze);
            Clients.Client(connectedUsers[mazeName][1]).StartGameFromJoin(jMaze);
        }

        public void play(string direction) {
            Move move = model.Play(direction, Context.ConnectionId);
            string otherPlayer;
            if (connectedUsers[move.GetMazeName()][0].Equals(Context.ConnectionId)) {
                otherPlayer = connectedUsers[move.GetMazeName()][1];
            } else {
                otherPlayer = connectedUsers[move.GetMazeName()][0];
            }
        }

        public void Close(string gameName) {
            
        }
    }
}