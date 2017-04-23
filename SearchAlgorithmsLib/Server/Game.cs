using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Server {
    public class Game {

        //members
        private SearchableMazeAdapter maze;
        private bool joinable;
        private List<TcpClient> players;

        public Game(SearchableMazeAdapter maze, TcpClient client) {
            if (maze == null) throw new ArgumentNullException(nameof(maze));
            this.maze = maze;
            joinable = true;
            players = new List<TcpClient>();
            players.Add(client);
        }

        //properties
        public SearchableMazeAdapter Maze => maze;

        public List<TcpClient> Players => players;

        public bool Joinable {
            get => joinable;
            set => joinable = value;
        }

        public void AddPlayer(TcpClient client) {
            players.Add(client);
            joinable = false;
        }
    }
}