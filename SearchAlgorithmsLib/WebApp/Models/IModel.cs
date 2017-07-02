using MazeLib;
using SearchAlgorithmsLib;
using System.Net.Sockets;

namespace WebApp.Models {
    /// <summary>
    /// Enum Algoritem
    /// </summary>
    public enum Algoritem { BFS, DFS };

    /// <summary>
    /// Interface IModel
    /// </summary>
    public interface IModel {

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
        SearchableMazeAdapter Start(string name, int rows, int cols, string client);

        /// <summary>
        /// Joins the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="client">The client.</param>
        /// <returns>SearchableMazeAdapter.</returns>
        SearchableMazeAdapter Join(string name, string client);

        /// <summary>
        /// Plays the specified step.
        /// </summary>
        /// <param name="step">The step.</param>
        /// <param name="client">The client.</param>
        Move Play(string step, string client);

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
        string Close(string name, string client);

        SearchableMazeAdapter GetMazeByName(string name);
    }
}
