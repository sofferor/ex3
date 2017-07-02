using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Web;

namespace WebApp.Models {
    /// <summary>
    /// Class Game.
    /// </summary>
    public class Game {

        //members
        /// <summary>
        /// The maze
        /// </summary>
        private SearchableMazeAdapter maze;
        /// <summary>
        /// The joinable
        /// </summary>
        private bool joinable;
        /// <summary>
        /// The players
        /// </summary>
        private List<string> players;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="maze">The maze.</param>
        /// <param name="client">The client.</param>
        /// <exception cref="System.ArgumentNullException">maze</exception>
        public Game(SearchableMazeAdapter maze, string client) {
            if (maze == null) throw new ArgumentNullException(nameof(maze));
            this.maze = maze;
            joinable = true;
            players = new List<string>();
            players.Add(client);
        }

        //properties
        /// <summary>
        /// Gets the maze.
        /// </summary>
        /// <value>The maze.</value>
        public SearchableMazeAdapter Maze => maze;

        /// <summary>
        /// Gets the players.
        /// </summary>
        /// <value>The players.</value>
        public List<string> Players => players;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Game"/> is joinable.
        /// </summary>
        /// <value><c>true</c> if joinable; otherwise, <c>false</c>.</value>
        public bool Joinable {
            get => joinable;
            set => joinable = value;
        }

        /// <summary>
        /// Adds the player.
        /// </summary>
        /// <param name="client">The client.</param>
        public void AddPlayer(string client) {
            players.Add(client);
            joinable = false;
        }
    }
}