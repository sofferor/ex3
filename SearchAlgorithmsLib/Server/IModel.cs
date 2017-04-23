using MazeLib;
using SearchAlgorithmsLib;
using System.Net.Sockets;

namespace Server {

    public enum Algoritem { BFS, DFS };

    public interface IModel {

        void SetController(IController c);

        SearchableMazeAdapter GenerateMaze(string name, int rows, int cols);

        Solution<Position> Solve(string name, Algoritem algoritem);

        SearchableMazeAdapter Start(string name, int rows, int cols, TcpClient client);

        string GamesList();
    }
}