using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Tests;

namespace Server {
    public class Game {
        private SearchableMazeAdapter maze;
        private bool joinable;
        private List<TcpClient> players;

        public Game(SearchableMazeAdapter maze) {
            if (maze == null) throw new ArgumentNullException(nameof(maze));
            this.maze = maze;
            joinable = true;
            players = new List<TcpClient>();
        }
    }
}