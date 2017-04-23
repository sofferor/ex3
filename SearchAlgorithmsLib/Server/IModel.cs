using MazeLib;
using SearchAlgorithmsLib;
namespace Server {

    public enum Algoritem { BFS, DFS };

    public interface IModel {

        SearchableMazeAdapter GenerateMaze(string name, int rows, int cols);

        Solution<Position> Solve(string name, Algoritem algoritem);

        string GamesList();
    }
}