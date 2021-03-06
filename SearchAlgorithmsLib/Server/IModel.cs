﻿using MazeLib;
using SearchAlgorithmsLib;
using System.Net.Sockets;

namespace Server {

    /// <summary>
    /// Enum Algoritem
    /// </summary>
    public enum Algoritem { BFS, DFS };

    /// <summary>
    /// Interface IModel
    /// </summary>
    public interface IModel {

        /// <summary>
        /// Sets the controller.
        /// </summary>
        /// <param name="c">The c.</param>
        void SetController(IController c);

        /// <summary>
        /// Generates the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <returns>SearchableMazeAdapter.</returns>
        SearchableMazeAdapter GenerateMaze(string name, int rows, int cols);

        /// <summary>
        /// Solves the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="algoritem">The algoritem.</param>
        /// <returns>Solution&lt;Position&gt;.</returns>
        Solution<Position> Solve(string name, Algoritem algoritem);

        /// <summary>
        /// Starts the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <param name="client">The client.</param>
        /// <returns>SearchableMazeAdapter.</returns>
        SearchableMazeAdapter Start(string name, int rows, int cols, TcpClient client);

        /// <summary>
        /// Joins the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="client">The client.</param>
        /// <returns>SearchableMazeAdapter.</returns>
        SearchableMazeAdapter Join(string name, TcpClient client);

        /// <summary>
        /// Plays the specified step.
        /// </summary>
        /// <param name="step">The step.</param>
        /// <param name="client">The client.</param>
        void Play(string step, TcpClient client);

        /// <summary>
        /// Gameses the list.
        /// </summary>
        /// <returns>System.String.</returns>
        string GamesList();

        /// <summary>
        /// Closes the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="client">The client.</param>
        /// <returns>System.String.</returns>
        string Close(string name, TcpClient client);
    }
}